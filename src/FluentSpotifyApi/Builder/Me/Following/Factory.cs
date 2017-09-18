using System.Collections.Generic;
using FluentSpotifyApi.Model;

namespace FluentSpotifyApi.Builder.Me.Following
{
    internal static class Factory
    {
        private const string EndpointName = "following";

        private const string ArtistEntityName = "artist";

        private const string UserEntityName = "user";

        public static IGetFollowedEntitiesBuilder<FollowedArtists> CreateGetFollowedArtistsBuilder(ContextData contextData, IEnumerable<object> routeValuesPrefix)
        {
            return new GetFollowedEntitiesBuilder<FollowedArtists>(contextData, routeValuesPrefix, EndpointName, ArtistEntityName);
        }

        public static IManageFollowedEntitiesBuilder CreateManageFollowedArtistsBuilder(ContextData contextData, IEnumerable<object> routeValuesPrefix, IEnumerable<string> ids)
        {
            return new ManageFollowedEntitiesBuilder(contextData, routeValuesPrefix, EndpointName, ArtistEntityName, ids);
        }

        public static IManageFollowedEntitiesBuilder CreateManageFollowedUsersBuilder(ContextData contextData, IEnumerable<object> routeValuesPrefix, IEnumerable<string> ids)
        {
            return new ManageFollowedEntitiesBuilder(contextData, routeValuesPrefix, EndpointName, UserEntityName, ids);
        }
    }
}
