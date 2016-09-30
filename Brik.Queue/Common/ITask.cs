using System;
using System.Threading.Tasks;
using System.Threading;

namespace Brik.Queue.Common
{
    /// <summary>
    /// Task interface
    /// </summary>
    public interface ITask
    {
        /// <summary>
        /// Delay task execution
        /// </summary>
        int Delay { get; }

        /// <summary>
        /// Cancelation token for task
        /// </summary>
        CancellationToken Cancellation { get; }

        /// <summary>
        /// Executes task action
        /// </summary>
        Task ExecuteAction();

        /// <summary>
        /// Execute task callback action
        /// </summary>
        Task ExecuteCallback();

        /// <summary>
        /// Execute task error callback
        /// </summary>
        /// <param name="e">Handled exception</param>
        Task ExecuteErrorCallback(Exception e);
    }

    /// <summary>
    /// Task with context
    /// </summary>
    public interface ITask<out TContext> : ITask
    {
        /// <summary>
        /// Task context
        /// </summary>
        TContext Context { get; }
    }
}