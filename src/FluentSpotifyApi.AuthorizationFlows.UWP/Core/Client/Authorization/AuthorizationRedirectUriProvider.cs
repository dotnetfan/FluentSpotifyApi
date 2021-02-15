﻿using System;
using FluentSpotifyApi.AuthorizationFlows.Core.Client.Authorization;
using Windows.Security.Authentication.Web;

namespace FluentSpotifyApi.AuthorizationFlows.UWP.Core.Client.Authorization
{
    internal class AuthorizationRedirectUriProvider : IAuthorizationRedirectUriProvider
    {
        public Uri Get()
        {
            return WebAuthenticationBroker.GetCurrentApplicationCallbackUri();
        }
    }
}
