using System;
using System.Collections.Generic;
using System.Linq;
using FluentSpotifyApi.AuthorizationFlows.Core.Client.Token.Exceptions;
using FluentSpotifyApi.AuthorizationFlows.Native.AuthorizationCode;
using FluentSpotifyApi.Core.Exceptions;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace FluentSpotifyApi.Sample.ACF.UWP.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IFluentSpotifyClient fluentSpotifyClient;

        private bool isLoading;

        private bool isLoaded;

        private bool isLoadPlaylistsCommunicationError;

        private bool isLoadPlaylistsCanceled;

        private bool isAccessRevoked;

        private List<PlaylistViewModel> playlists;

        private RelayCommand loadCommand;

        private RelayCommand loadPlaylistsCommand;

        public MainViewModel(IAuthenticationManager authenticationManager, IFluentSpotifyClient fluentSpotifyClient)
        {
            this.fluentSpotifyClient = fluentSpotifyClient;

            this.CancelableActionViewModel = new CancelableActionViewModel();
            this.CancelableActionViewModel.PropertyChanged += this.CancelableActionViewModel_PropertyChanged;

            this.LoginViewModel = new LoginViewModel(authenticationManager, this.CancelableActionViewModel);
            this.LoginViewModel.PropertyChanged += this.LoginViewModel_PropertyChanged;
        }

        public LoginViewModel LoginViewModel { get; }

        public CancelableActionViewModel CancelableActionViewModel { get; }

        public bool IsLoading
        {
            get
            {
                return this.isLoading;
            }

            set
            {
                if (this.Set(() => this.IsLoading, ref this.isLoading, value))
                {
                    this.LoadCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public bool IsLoaded
        {
            get
            {
                return this.isLoaded;
            }

            set
            {
                if (this.Set(() => this.IsLoaded, ref this.isLoaded, value))
                {
                    this.LoadCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public bool IsLoadPlaylistsCommunicationError
        {
            get
            {
                return this.isLoadPlaylistsCommunicationError;
            }

            set
            {
                this.Set(() => this.IsLoadPlaylistsCommunicationError, ref this.isLoadPlaylistsCommunicationError, value);
            }
        }

        public bool IsLoadPlaylistsCanceled
        {
            get
            {
                return this.isLoadPlaylistsCanceled;
            }

            set
            {
                this.Set(() => this.IsLoadPlaylistsCanceled, ref this.isLoadPlaylistsCanceled, value);
            }
        }

        public bool IsAccessRevoked
        {
            get
            {
                return this.isAccessRevoked;
            }

            set
            {
                this.Set(() => this.IsAccessRevoked, ref this.isAccessRevoked, value);
            }
        }

        public List<PlaylistViewModel> Playlists
        {
            get
            {
                return this.playlists;
            }

            set
            {
                this.Set(() => this.Playlists, ref this.playlists, value);
            }
        }

        public RelayCommand LoadCommand
        {
            get
            {
                return this.loadCommand
                    ?? (this.loadCommand = new RelayCommand(
                        async () =>
                        {
                            this.IsLoading = true;
                            try
                            {
                                await this.LoginViewModel.LogInAsync();
                            }
                            finally
                            {
                                this.IsLoading = false;
                                this.IsLoaded = true;
                            }

                            this.LoadPlaylistsCommand.Execute(null);
                        },
                        () => !this.IsLoading && !this.IsLoaded));
            }
        }

        public RelayCommand LoadPlaylistsCommand
        {
            get
            {
                return this.loadPlaylistsCommand
                    ?? (this.loadPlaylistsCommand = new RelayCommand(
                        async () =>
                        {
                            await this.CancelableActionViewModel.ExecuteAsync(async cancellationToken =>
                            {
                                this.ResetPlaylists();

                                var userId = this.LoginViewModel.User.Id;

                                try
                                {
                                    this.Playlists = (await this.fluentSpotifyClient.Me.Playlists
                                        .GetAsync(limit: 20, offset: 0, cancellationToken: cancellationToken))
                                        .Items
                                        .Select(item => new PlaylistViewModel(item, item.Owner?.Id == userId))
                                        .ToList();
                                }
                                catch (OperationCanceledException)
                                {
                                    this.IsLoadPlaylistsCanceled = true;
                                }
                                catch (SpotifyInvalidRefreshTokenException)
                                {
                                    this.IsAccessRevoked = true;
                                }
                                catch (SpotifyCommunicationException)
                                {
                                    this.IsLoadPlaylistsCommunicationError = true;
                                }
                            });
                        },
                        () => !this.CancelableActionViewModel.IsExecuting && !this.LoginViewModel.IsLoggingInOrLoggingOut && this.LoginViewModel.IsLoggedIn));
            }
        }

        private void CancelableActionViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(this.CancelableActionViewModel.IsExecuting))
            {
                this.LoadPlaylistsCommand.RaiseCanExecuteChanged();
            }
        }

        private void LoginViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(this.LoginViewModel.IsLoggingInOrLoggingOut))
            {
                this.LoadPlaylistsCommand.RaiseCanExecuteChanged();
                this.LoadPlaylistsCommand.Execute(null);
            }
            else if (e.PropertyName == nameof(this.LoginViewModel.IsLoggedIn))
            {
                this.LoadPlaylistsCommand.RaiseCanExecuteChanged();
                if (!this.LoginViewModel.IsLoggedIn)
                {
                    this.ResetPlaylists();
                }
            }
        }

        private void ResetPlaylists()
        {
            this.Playlists = null;
            this.IsLoadPlaylistsCommunicationError = false;
            this.IsLoadPlaylistsCanceled = false;
            this.IsAccessRevoked = false;
        }
    }
}
