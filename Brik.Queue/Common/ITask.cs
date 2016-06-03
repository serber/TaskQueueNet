using System;
using System.Threading;
using System.Threading.Tasks;

namespace Brik.Queue.Common
{
    public interface ITask
    {
        int Delay { get; }

        CancellationToken Cancellation { get; set; }

        Task ExecuteAction();

        Task ExecuteCallback();

        Task ExecuteErrorCallback(Exception e);
    }

    /// <summary>
    /// Task interface
    /// </summary>
    public interface ITask<out TContext> : ITask
    {
        TContext Context { get; }
    }
}