using System;
using System.Threading;
using System.Threading.Tasks;
using Brik.Queue.Common;

namespace Brik.Queue
{
    public abstract class BaseTaskHandler : ITaskHandler
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
                await TaskAction(task);
                //---
                cancellationToken.ThrowIfCancellationRequested();
                //---
                await TaskCallback(task);
                //---
            }
            catch (Exception e)
            {
                await TaskError(task, e);
            }
        }

        protected abstract Task TaskAction(ITask task);

        protected abstract Task TaskCallback(ITask task);

        protected abstract Task TaskError(ITask task, Exception e);
    }
}