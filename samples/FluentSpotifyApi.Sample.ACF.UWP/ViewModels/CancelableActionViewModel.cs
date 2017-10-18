using System;
using System.Threading;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace FluentSpotifyApi.Sample.ACF.UWP.ViewModels
{
    public class CancelableActionViewModel : ViewModelBase
    {
        private bool isExecuting;

        private (TaskCompletionSource<int> TaskCompletionSource, CancellationTokenSource CancellationTokenSource) awaitable;

        public bool IsExecuting
        {
            get
            {
                return this.isExecuting;
            }

            set
            {
                this.Set(() => this.IsExecuting, ref this.isExecuting, value);
            }
        }

        public async Task ExecuteAsync(Func<CancellationToken, Task> action)
        {
            if (this.IsExecuting)
            {
                throw new InvalidOperationException("There is already an operation in progress.");
            }

            this.IsExecuting = true;
            try
            {
                if (this.awaitable.CancellationTokenSource != null && this.awaitable.CancellationTokenSource.IsCancellationRequested)
                {
                    this.awaitable.CancellationTokenSource.Dispose();
                    this.awaitable.CancellationTokenSource = null;
                }

                this.awaitable.CancellationTokenSource = this.awaitable.CancellationTokenSource ?? new CancellationTokenSource();
                this.awaitable.TaskCompletionSource = new TaskCompletionSource<int>();

                try
                {
                    await action(this.awaitable.CancellationTokenSource.Token);
                }
                finally
                {
                    this.awaitable.TaskCompletionSource.SetResult(0);
                    this.awaitable.TaskCompletionSource = null;
                }
            }
            finally
            {
                this.IsExecuting = false;
            }
        }

        /// <summary>
        /// This method assumes that there is already a flag set that prevents other commands from being executed.
        /// </summary>
        /// <returns></returns>
        public async Task CancelAsync()
        {
            if (this.IsExecuting)
            {
                this.awaitable.CancellationTokenSource.Cancel();

                await this.awaitable.TaskCompletionSource.Task;
            }
        }
    }
}
