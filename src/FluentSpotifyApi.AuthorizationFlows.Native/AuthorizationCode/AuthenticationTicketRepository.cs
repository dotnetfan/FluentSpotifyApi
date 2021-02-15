using System;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.AuthorizationFlows.Core.Exceptions;
using FluentSpotifyApi.AuthorizationFlows.Core.Model;

namespace FluentSpotifyApi.AuthorizationFlows.Native.AuthorizationCode
{
    internal class AuthenticationTicketRepository : IAuthenticationTicketRepository
    {
        private const int Version = 2;

        private readonly IAuthenticationTicketStorage authenticationTicketStorage;

        public AuthenticationTicketRepository(IAuthenticationTicketStorage authenticationTicketStorage)
        {
            this.authenticationTicketStorage = authenticationTicketStorage;
        }

        public async Task<AuthenticationTicket> GetAsync(CancellationToken cancellationToken)
        {
            var (isSuccess, value) = await this.authenticationTicketStorage.TryGetAsync(cancellationToken).ConfigureAwait(false);
            if (!isSuccess)
            {
                return null;
            }

            try
            {
                using (var payload = JsonDocument.Parse(value))
                {
                    var element = payload.RootElement;
                    var version = element.GetProperty("version").GetInt32();
                    return this.GetAuthenticationTicketFromJsonElement(element, version);
                }
            }
            catch (Exception)
            {
                await this.authenticationTicketStorage.RemoveAsync(cancellationToken).ConfigureAwait(false);
                return null;
            }
        }

        public async Task SaveAsync(AuthenticationTicket authenticationTicket, CancellationToken cancellationToken)
        {
            var repositoryItem = new AuthenticationTicketRepositoryItem
            {
                Version = Version,
                RefreshToken = authenticationTicket.RefreshToken,
                AccessToken = authenticationTicket.AccessToken.Token,
                ExpiresAt = authenticationTicket.AccessToken.ExpiresAt,
                UserClaims = authenticationTicket.UserClaims.ToDictionary(c => c.Type, c => c.Value)
            };

            await this.authenticationTicketStorage.SaveAsync(JsonSerializer.Serialize(repositoryItem), cancellationToken).ConfigureAwait(false);
        }

        public Task<bool> DeleteAsync(CancellationToken cancellationToken)
        {
            return this.authenticationTicketStorage.RemoveAsync(cancellationToken);
        }

        private AuthenticationTicket GetAuthenticationTicketFromJsonElement(JsonElement element, int version)
        {
            switch (version)
            {
                case Version:
                    var repositoryItem = this.ToObject<AuthenticationTicketRepositoryItem>(element);

                    if (repositoryItem == null)
                    {
                        throw new SpotifyMalformedAuthenticationTicketException("Unable to get authentication ticket.");
                    }

                    if (string.IsNullOrEmpty(repositoryItem.RefreshToken) ||
                        string.IsNullOrEmpty(repositoryItem.AccessToken) ||
                        repositoryItem.ExpiresAt == null)
                    {
                        throw new SpotifyMalformedAuthenticationTicketException("Unable to get authentication tokens.");
                    }

                    if (repositoryItem.UserClaims?.Any(kvp => string.IsNullOrEmpty(kvp.Key) || string.IsNullOrEmpty(kvp.Value)) == true)
                    {
                        throw new SpotifyMalformedAuthenticationTicketException("Unable to get user claims.");
                    }

                    var userClaims = new UserClaims(repositoryItem.UserClaims?.Select(kvp => new UserClaim(kvp.Key, kvp.Value)));
                    if (!userClaims.HasClaim(UserClaimTypes.Id))
                    {
                        throw new SpotifyMalformedAuthenticationTicketException("Unable to get User ID.");
                    }

                    return new AuthenticationTicket(
                        repositoryItem.RefreshToken,
                        new AccessToken(repositoryItem.AccessToken, repositoryItem.ExpiresAt.Value),
                        userClaims);
                default:
                    throw new SpotifyMalformedAuthenticationTicketException($"Stored authentication ticket has version {version} that is not supported.");
            }
        }

        private T ToObject<T>(JsonElement element)
        {
            var json = element.GetRawText();
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}
