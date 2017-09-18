using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.AuthorizationFlows.AuthorizationCode.Native;
using FluentSpotifyApi.AuthorizationFlows.Core.Client.Authorization;
using FluentSpotifyApi.Core.Options;
using Windows.Security.Credentials;

namespace FluentSpotifyApi.AuthorizationFlows.UWP.AuthorizationCode
{
    internal class SecureStorage : ISecureStorage
    {
        private const string ResourceName = "FluentSpotifyApi_Auth";

        private readonly IOptionsProvider<IAuthorizationOptions> authorizationOptionsProvider;

        public SecureStorage(IOptionsProvider<IAuthorizationOptions> authorizationOptionsProvider)
        {
            this.authorizationOptionsProvider = authorizationOptionsProvider;
        }

        [SuppressMessage("Microsoft.StyleCop.CSharp.SpacingRules", "SA1009:ClosingParenthesisMustBeSpacedCorrectly", Justification = "C# 7 Tuples")]
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

            return Task.FromResult(0);
        }

        public Task RemoveAsync(CancellationToken cancellationToken)
        {
            var vault = new PasswordVault();
            var userName = this.GetUserName();

            this.RemoveInternal(vault, userName);

            return Task.FromResult(0);
        }

        private void RemoveInternal(PasswordVault vault, string userName)
        {
            var credentials = this.RetrieveInternal(vault, userName);
            if (credentials != null)
            {
                vault.Remove(credentials);
            }
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

        private string GetUserName()
        {
            return this.authorizationOptionsProvider.Get().ClientId;
        }
    }
}
