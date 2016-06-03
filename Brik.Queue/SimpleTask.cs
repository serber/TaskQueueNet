using System;
using System.Threading;
using System.Threading.Tasks;
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

        #region C-tor

        /// <summary>
        /// Creates <see cref="SimpleTask{TContext}"/>
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
        /// Creates <see cref="SimpleTask{TContext}"/>
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
        /// Creates <see cref="SimpleTask{TContext}"/>
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

        public Task ExecuteAction()
        {
            return Task.Run(() => { Action(); }, Cancellation);
        }

        public Task ExecuteCallback()
        {
            return Task.Run(() => { Callback(); }, Cancellation);
        }

        public Task ExecuteErrorCallback(Exception e)
        {
            return Task.Run(() => { ErrorCallback(e); }, Cancellation);
        }
    }

    public sealed class SimpleTask<TContext> : ITask<TContext>
    {
        #region Public methods

        public Action<TContext> Action { get; }

        public Action<TContext> Callback { get; }

        public Action<TContext, Exception> ErrorCallback { get; }

        public TContext Context { get; }

        public int Delay { get; }

        public CancellationToken Cancellation { get; set; }

        #endregion

        #region C-tor

        /// <summary>
        /// Creates <see cref="SimpleTask{TContext}"/>
        /// </summary>
        /// <param name="context"></param>
        /// <param name="action">Main action delegate</param>
        /// <param name="delay">Delay time</param>
        /// <param name="cancellation">Cancelation token</param>
        public SimpleTask(TContext context, Action<TContext> action, int delay = 0, CancellationToken cancellation = default(CancellationToken))
                : this(context, action, delegate { }, delay, cancellation)
        {
            Action = action;
        }

        /// <summary>
        /// Creates <see cref="SimpleTask{TContext}"/>
        /// </summary>
        /// <param name="context"></param>
        /// <param name="action">Main action delegate</param>
        /// <param name="callback">Callback action delegate</param>
        /// <param name="delay">Delay time</param>
        /// <param name="cancellation">Cancelation token</param>
        public SimpleTask(TContext context, Action<TContext> action, Action<TContext> callback, int delay = 0, CancellationToken cancellation = default(CancellationToken))
                : this(context, action, delegate { }, delegate { }, delay, cancellation)
        {
            Action = action;
            Callback = callback;
        }

        /// <summary>
        /// Creates <see cref="SimpleTask{TContext}"/>
        /// </summary>
        /// <param name="context"></param>
        /// <param name="action">Main action delegate</param>
        /// <param name="callback">Callback action delegate</param>
        /// <param name="errorCallback">Error callback action delegate</param>
        /// <param name="delay">Delay time</param>
        /// <param name="cancellation">Cancelation token</param>
        public SimpleTask(TContext context, Action<TContext> action, Action<TContext> callback, Action<TContext, Exception> errorCallback, int delay = 0, CancellationToken cancellation = default(CancellationToken))
        {
            Context = context;
            Action = action;
            Callback = callback;
            ErrorCallback = errorCallback;
            Delay = delay;
            Cancellation = cancellation;
        }

        #endregion

        public Task ExecuteAction()
        {
            return Task.Run(() => { Action(Context); }, Cancellation);
        }

        public Task ExecuteCallback()
        {
            return Task.Run(() => { Callback(Context); }, Cancellation);
        }

        public Task ExecuteErrorCallback(Exception e)
        {
            return Task.Run(() => { ErrorCallback(Context, e); }, Cancellation);
        }
    }
}