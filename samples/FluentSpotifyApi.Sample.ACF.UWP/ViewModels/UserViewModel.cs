using FluentSpotifyApi.Core.Model;
using GalaSoft.MvvmLight;

namespace FluentSpotifyApi.Sample.ACF.UWP.ViewModels
{
    public class UserViewModel : ViewModelBase
    {
        private readonly PrivateUser privateUser;

        public UserViewModel(PrivateUser privateUser)
        {
            this.privateUser = privateUser;
        }

        public string Id => this.privateUser.Id;

        public string DisplayName => (string.IsNullOrEmpty(this.privateUser.DisplayName) ? this.privateUser.Id : this.privateUser.DisplayName);
    }
}
