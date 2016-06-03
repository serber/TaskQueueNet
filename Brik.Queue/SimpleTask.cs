using System;
using System.Threading;
using Brik.Queue.Common;

namespace Brik.Queue
{
    public sealed class SimpleTask : ITask
    {
        #region Public methods

        public Action Action { get; }

        public Action Callback { get; }

        public Action<Exception> ErrorCallback { get; }

        public int Delay { get; }

        public CancellationToken Cancellation { get; set; }

        #endregion

        #region Public methods

        /// <summary>
        /// Creates <see cref="SimpleTask"/>
        /// </summary>
        /// <param name="action">Main action delegate</param>
        /// <param name="delay">Delay time</param>
        /// <param name="cancellation">Cancelation token</param>
        public SimpleTask(Action action, int delay = 0, CancellationToken cancellation = default(CancellationToken))
            : this(action, delegate { }, delay, cancellation)
        {
            Action = action;
        }

        /// <summary>
        /// Creates <see cref="SimpleTask"/>
        /// </summary>
        /// <param name="action">Main action delegate</param>
        /// <param name="callback">Callback action delegate</param>
        /// <param name="delay">Delay time</param>
        /// <param name="cancellation">Cancelation token</param>
        public SimpleTask(Action action, Action callback, int delay = 0, CancellationToken cancellation = default(CancellationToken))
            : this(action, delegate { }, delegate { }, delay, cancellation)
        {
            Action = action;
            Callback = callback;
        }

        /// <summary>
        /// Creates <see cref="SimpleTask"/>
        /// </summary>
        /// <param name="action">Main action delegate</param>
        /// <param name="callback">Callback action delegate</param>
        /// <param name="errorCallback">Error callback action delegate</param>
        /// <param name="delay">Delay time</param>
        /// <param name="cancellation">Cancelation token</param>
        public SimpleTask(Action action, Action callback, Action<Exception> errorCallback, int delay = 0, CancellationToken cancellation = default(CancellationToken))
        {
            Action = action;
            Callback = callback;
            ErrorCallback = errorCallback;
            Delay = delay;
            Cancellation = cancellation;
        }

        #endregion
    }
}