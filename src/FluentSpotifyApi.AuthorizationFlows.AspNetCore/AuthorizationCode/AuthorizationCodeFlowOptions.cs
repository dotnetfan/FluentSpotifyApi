using System;
using FluentSpotifyApi.AuthorizationFlows.Core.Client.Token;

namespace FluentSpotifyApi.AuthorizationFlows.AspNetCore.AuthorizationCode
{
    internal sealed class AuthorizationCodeFlowOptions : ISpotifyTokenClientOptions
    {
        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public Uri TokenEndpoint { get; set; }
    }
}
