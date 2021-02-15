using System;
using FluentSpotifyApi.AuthorizationFlows.Core.Time;
using Microsoft.AspNetCore.Authentication;

namespace FluentSpotifyApi.AuthorizationFlows.AspNetCore.AuthorizationCode
{
    internal class ClockFromSystemClock : IClock
    {
        private readonly ISystemClock systemClock;

        public ClockFromSystemClock(ISystemClock systemClock)
        {
            this.systemClock = systemClock;
        }

        public DateTimeOffset GetUtcNow() => this.systemClock.UtcNow;
    }
}
