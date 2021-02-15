using System;

namespace FluentSpotifyApi.AuthorizationFlows.Core.Time
{
    internal class Clock : IClock
    {
        public DateTimeOffset GetUtcNow()
        {
            return DateTime.UtcNow;
        }
    }
}
