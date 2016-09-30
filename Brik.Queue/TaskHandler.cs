using System.Threading.Tasks;
using Brik.Queue.Common;

namespace Brik.Queue
{
    /// <summary>
    /// Simple task handler with non parallel task execution
    /// </summary>
    public sealed class TaskHandler : BaseTaskHandler
    {
        public override async Task HandleAsync(ITask task)
        {
            await HandleTaskAsync(task);
        }
    }

    /// <summary>
    /// Simple task handler with parallel task execution
    /// </summary>
    public sealed class ParallelTaskHandler : BaseTaskHandler
    {
        public override Task HandleAsync(ITask task)
        {
            return Task.Factory.StartNew(() => HandleTaskAsync(task));
        }
    }
}