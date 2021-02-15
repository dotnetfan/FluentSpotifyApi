using FluentSpotifyApi.AuthorizationFlows.Native.AuthorizationCode;
using GalaSoft.MvvmLight;

namespace FluentSpotifyApi.Sample.ACF.UWP.ViewModels
{
    public class UserViewModel : ViewModelBase
    {
        public UserViewModel(UserClaims userClaims)
        {
            this.Id = userClaims[UserClaimTypes.Id].Value;
            this.DisplayName = (userClaims.GetClaimOrDefault(UserClaimTypes.DisplayName) ?? userClaims[UserClaimTypes.Id]).Value;
        }

        public string Id { get; }

        public string DisplayName { get; }
    }
}
