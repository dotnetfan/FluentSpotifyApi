using System;
using FluentSpotifyApi.AuthorizationFlows.Core.Client.Token;
using FluentSpotifyApi.Core.Options;

namespace FluentSpotifyApi.AuthorizationFlows.AspNetCore.AuthorizationCode
{
    internal sealed class AspNetCoreAuthorizationCodeFlowOptions : ITokenClientOptions, IValidatable
    {
        public AspNetCoreAuthorizationCodeFlowOptions()
        {
        }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public Uri TokenEndpoint { get; set; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(this.ClientId))
            {
                throw new ArgumentException($"{nameof(this.ClientId)} must be a non-empty string.", nameof(this.ClientId));
            }

            if (string.IsNullOrEmpty(this.ClientSecret))
            {
                throw new ArgumentException($"{nameof(this.ClientSecret)} must be a non-empty string.", nameof(this.ClientSecret));
            }

            if (this.TokenEndpoint == null)
            {
                throw new ArgumentNullException(nameof(this.TokenEndpoint));
            }
        }
    }
}
