using System;
using System.Threading;

namespace Brik.Queue.Common
{
    /// <summary>
    /// Task interface
    /// </summary>
    public interface ITask
    {
        Action Action { get; }

        Action Callback { get; }

        Action<Exception> ErrorCallback { get; }

        int Delay { get; }
        
        CancellationToken Cancellation { get; set; }
    }
}