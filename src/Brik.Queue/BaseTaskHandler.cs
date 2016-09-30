using System;
using System.Threading;
using System.Threading.Tasks;
using Brik.Queue.Common;

namespace Brik.Queue
{
    /// <summary>
    /// Base abstract task handler implementation
    /// </summary>
    public abstract class BaseTaskHandler : ITaskHandler
    {
        public abstract Task HandleAsync(ITask task);

        /// <summary>
        /// Handle task execution async
        /// </summary>
        /// <param name="task"><see cref="ITask"/> instance</param>
        protected async Task HandleTaskAsync(ITask task)
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
                await task.ExecuteAction();
                //---
                cancellationToken.ThrowIfCancellationRequested();
                //---
                await task.ExecuteCallback();
                //---
            }
            catch (Exception e)
            {
                await task.ExecuteErrorCallback(e);
            }
        }
    }
}