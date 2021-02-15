using System;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.AuthorizationFlows.Native.AuthorizationCode;
using FluentSpotifyApi.Core.Options;
using Windows.Security.Credentials;

namespace FluentSpotifyApi.AuthorizationFlows.UWP.AuthorizationCode
{
    internal class AuthenticationTicketStorage : IAuthenticationTicketStorage
    {
        private const string ResourceName = "FluentSpotifyApi_Auth";

        private readonly IOptionsProvider<SpotifyAuthorizationCodeFlowOptions> optionsProvider;

        public AuthenticationTicketStorage(IOptionsProvider<SpotifyAuthorizationCodeFlowOptions> optionsProvider)
        {
            this.optionsProvider = optionsProvider;
        }

        public Task<(bool IsSuccess, string Value)> TryGetAsync(CancellationToken cancellationToken)
        {
            var vault = new PasswordVault();
            var userName = this.GetUserName();

            var result = this.RetrieveInternal(vault, userName);
            if (result != null)
            {
                return Task.FromResult((true, result.Password));
            }
            else
            {
                return Task.FromResult((false, (string)null));
            }
        }

        public Task SaveAsync(string value, CancellationToken cancellationToken)
        {
            var vault = new PasswordVault();
            var userName = this.GetUserName();

            this.RemoveInternal(vault, userName);
            vault.Add(new PasswordCredential(ResourceName, userName, value));

            return Task.CompletedTask;
        }

        public Task<bool> RemoveAsync(CancellationToken cancellationToken)
        {
            var vault = new PasswordVault();
            var userName = this.GetUserName();

            var result = this.RemoveInternal(vault, userName);

            return Task.FromResult(result);
        }

        private bool RemoveInternal(PasswordVault vault, string userName)
        {
            var credentials = this.RetrieveInternal(vault, userName);
            if (credentials != null)
            {
                vault.Remove(credentials);
                return true;
            }

            return false;
        }

        private PasswordCredential RetrieveInternal(PasswordVault vault, string userName)
        {
            PasswordCredential credentials = null;

            try
            {
                credentials = vault.Retrieve(ResourceName, userName);
            }
            catch (Exception)
            {
            }

            return credentials;
        }

        private string GetUserName() => this.optionsProvider.Get().ClientId;
    }
}
