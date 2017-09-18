using System.Threading.Tasks;
using FluentSpotifyApi.AuthorizationFlows.AuthorizationCode.Native;
using FluentSpotifyApi.Core.Exceptions;
using FluentSpotifyApi.Core.Model;
using FluentSpotifyApi.Sample.ACF.UWP.Common;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace FluentSpotifyApi.Sample.ACF.UWP.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IAuthenticationManager authenticationManager;

        private readonly CancelableActionViewModel cancelableActionViewModel;

        private bool isLoggedIn;

        private UserViewModel user;

        private bool isLoginServiceError;

        private bool isLoggingInOrLoggingOut;

        private RelayCommand loginCommand;

        private RelayCommand logoutCommand;

        public LoginViewModel(IAuthenticationManager authenticationManager, CancelableActionViewModel cancelableActionViewModel)
        {
            this.authenticationManager = authenticationManager;
            this.cancelableActionViewModel = cancelableActionViewModel;
        }

        public bool IsLoggingInOrLoggingOut
        {
            get
            {
                return this.isLoggingInOrLoggingOut;
            }

            set
            {
                if (this.Set(() => this.IsLoggingInOrLoggingOut, ref this.isLoggingInOrLoggingOut, value))
                {
                    this.LoginCommand.RaiseCanExecuteChanged();
                    this.LogoutCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public bool IsLoggedIn
        {
            get
            {
                return this.isLoggedIn;
            }

            set
            {
                if (this.Set(() => this.IsLoggedIn, ref this.isLoggedIn, value))
                {
                    this.LoginCommand.RaiseCanExecuteChanged();
                    this.LogoutCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public UserViewModel User
        {
            get
            {
                return this.user;
            }

            set
            {
                this.Set(() => this.User, ref this.user, value);
            }
        }

        public bool IsLoginServiceError
        {
            get
            {
                return this.isLoginServiceError;
            }

            set
            {
                this.Set(() => this.IsLoginServiceError, ref this.isLoginServiceError, value);
            }
        }

        public RelayCommand LoginCommand
        {
            get
            {
                return this.loginCommand
                    ?? (this.loginCommand = new RelayCommand(
                        async () =>
                        {
                            this.IsLoggingInOrLoggingOut = true;
                            try
                            {
                                await this.LogInAsync();
                            }
                            finally
                            {
                                this.IsLoggingInOrLoggingOut = false;
                            }
                        },
                        () => !this.IsLoggingInOrLoggingOut && !this.IsLoggedIn));
            }
        }

        public RelayCommand LogoutCommand
        {
            get
            {
                return this.logoutCommand
                    ?? (this.logoutCommand = new RelayCommand(
                        async () =>
                        {
                            this.IsLoggingInOrLoggingOut = true;
                            try
                            {
                                await this.cancelableActionViewModel.CancelAsync();
                                await this.authenticationManager.RemoveSessionAsync();

                                this.UpdateViewModel(false, null);
                            }
                            finally
                            {
                                this.IsLoggingInOrLoggingOut = false;
                            }
                        },
                        () => !this.IsLoggingInOrLoggingOut && this.IsLoggedIn));
            }
        }

        public async Task LogInAsync()
        {
            this.IsLoginServiceError = false;
            try
            {
                await this.authenticationManager.RestoreSessionOrAuthorizeUserAsync();
                this.UpdateViewModel(true, this.authenticationManager.GetUser());
            }
            catch (SpotifyServiceException)
            {
                this.IsLoginServiceError = true;
            }
        }

        private void UpdateViewModel(bool isLoggedIn, PrivateUser user)
        {
            this.IsLoggedIn = isLoggedIn;
            this.User = isLoggedIn ? new UserViewModel(user) : null;
        }
    }
}
