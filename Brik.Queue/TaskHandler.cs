using System;
using System.Threading;
using System.Threading.Tasks;
using Brik.Queue.Common;

namespace Brik.Queue
{
    public sealed class TaskHandler : ITaskHandler
    {
        public Task HandleAsync(ITask task)
        {
            return Task.Factory.StartNew(() => HandleTask(task));
        }

        private async Task HandleTask(ITask task)
        {
            try
            {
                CancellationToken cancellationToken = task.Cancellation;
                cancellationToken.ThrowIfCancellationRequested();
                //---
                if (task.Delay > 0)
                {
                    await Task.Delay(task.Delay, task.Cancellation);
                    //---
                    cancellationToken.ThrowIfCancellationRequested();
                }
                //---
                await Task.Run(task.Action, task.Cancellation);
                //---
                cancellationToken.ThrowIfCancellationRequested();
                //---
                await Task.Run(() => task.Callback(), cancellationToken);
                //---
            }
            catch (Exception e)
            {
                task.ErrorCallback(e);
            }
        }
    }
}