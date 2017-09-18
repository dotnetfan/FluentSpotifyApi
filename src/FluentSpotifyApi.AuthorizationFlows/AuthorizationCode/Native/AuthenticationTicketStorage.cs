using System;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FluentSpotifyApi.AuthorizationFlows.AuthorizationCode.Native
{
    internal class AuthenticationTicketStorage : IAuthenticationTicketStorage
    {
        private const int Version = 1;

        private readonly ISecureStorage secureStorage;

        public AuthenticationTicketStorage(ISecureStorage secureStorage)
        {
            this.secureStorage = secureStorage;
        }

        public async Task<AuthenticationTicket> GetAsync(CancellationToken cancellationToken)
        {
            var result = await this.secureStorage.TryGetAsync(cancellationToken).ConfigureAwait(false);
            if (!result.IsSuccess)
            {
                return null;
            }

            JObject payload;
            int payloadVersion;
            try
            {
                payload = JObject.Parse(result.Value);
                payloadVersion = payload.Value<int>("version");

                return this.GetAuthenticationTicketFromJObject(payload, payloadVersion, cancellationToken);
            }
            catch (Exception)
            {
                await this.secureStorage.RemoveAsync(cancellationToken).ConfigureAwait(false);
                return null;
            }            
        }

        public Task SaveAsync(AuthenticationTicket authenticationTicket, CancellationToken cancellationToken)
        {
            var storageItem = new AuthenticationTicketStorageItem
            {
                Version = Version,
                AuthorizationKey = authenticationTicket.AuthorizationKey,
                User = authenticationTicket.User
            };

            return this.secureStorage.SaveAsync(JsonConvert.SerializeObject(storageItem), cancellationToken);
        }

        public Task DeleteAsync(CancellationToken cancellationToken)
        {
            return this.secureStorage.RemoveAsync(cancellationToken);
        }

        private AuthenticationTicket GetAuthenticationTicketFromJObject(JObject payload, int payloadVersion, CancellationToken cancellationToken)
        {
            switch (payloadVersion)
            {
                case Version:
                    AuthenticationTicketStorageItem storageItem = payload.ToObject<AuthenticationTicketStorageItem>();

                    if (string.IsNullOrEmpty(storageItem.AuthorizationKey) || string.IsNullOrEmpty(storageItem.User?.Id))
                    {
                        throw new InvalidOperationException("Stored authentication ticket is not valid.");
                    }

                    return new AuthenticationTicket(storageItem.AuthorizationKey, null, storageItem.User);
                default:
                    throw new ArgumentOutOfRangeException(nameof(payloadVersion), payloadVersion, "Unknown version number.");
            }
        }
    }
}
