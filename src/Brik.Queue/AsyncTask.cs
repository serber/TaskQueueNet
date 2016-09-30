using System;
using System.Threading;
using System.Threading.Tasks;
using Brik.Queue.Common;

namespace Brik.Queue
{
    /// <summary>
    /// Async task implemetation
    /// </summary>
    public sealed class AsyncTask : ITask
    {
        #region Private fields

        private readonly Func<Task> _action;

        private readonly Func<Task> _callBack;

        private readonly Func<Exception, Task> _errorCallBack;

        #endregion

        #region C-tor

        /// <summary>
        /// Creates <see cref="AsyncTask"/>
        /// </summary>
        /// <param name="action">Main action delegate</param>
        /// <param name="delay">Delay time</param>
        /// <param name="cancellation">Cancelation token</param>
        public AsyncTask(Func<Task> action, int delay = 0, CancellationToken cancellation = default(CancellationToken))
                       : this(action, () => Task.FromResult(0), delay, cancellation)
        {
        }

        /// <summary>
        /// Creates <see cref="AsyncTask"/>
        /// </summary>
        /// <param name="action">Main action delegate</param>
        /// <param name="callback">Callback action delegate</param>
        /// <param name="delay">Delay time</param>
        /// <param name="cancellation">Cancelation token</param>
        public AsyncTask(Func<Task> action, Func<Task> callback, int delay = 0, CancellationToken cancellation = default(CancellationToken))
                       : this(action, callback, delegate { return Task.FromResult(0); }, delay, cancellation)
        {
        }

        /// <summary>
        /// Creates <see cref="AsyncTask"/>
        /// </summary>
        /// <param name="action">Main action delegate</param>
        /// <param name="callback">Callback action delegate</param>
        /// <param name="errorCallback">Error callback action delegate</param>
        /// <param name="delay">Delay task execution time</param>
        /// <param name="cancellation">Cancelation token</param>
        public AsyncTask(Func<Task> action, Func<Task> callback,
                         Func<Exception, Task> errorCallback,
                         int delay = 0,
                         CancellationToken cancellation = default(CancellationToken))
        {
            _action = action;
            _callBack = callback;
            _errorCallBack = errorCallback;

            Delay = delay;
            Cancellation = cancellation;
        }

        #endregion

        public int Delay { get; }

        public CancellationToken Cancellation { get; }

        public async Task ExecuteAction()
        {
            await _action();
        }

        public async Task ExecuteCallback()
        {
            await _callBack();
        }

        public async Task ExecuteErrorCallback(Exception e)
        {
            await _errorCallBack(e);
        }
    }

    /// <summary>
    /// Implements async task with context
    /// </summary>
    /// <typeparam name="TContext">Context type</typeparam>
    public sealed class AsyncTask<TContext> : ITask<TContext>
    {
        #region Private fields

        private readonly Func<TContext, Task> _action;

        private readonly Func<TContext, Task> _callBack;

        private readonly Func<TContext, Exception, Task> _errorCallBack;

        #endregion

        #region C-tor

        /// <summary>
        /// Creates <see cref="AsyncTask{TContext}"/>
        /// </summary>
        /// <param name="context"></param>
        /// <param name="action">Main action delegate</param>
        /// <param name="delay">Delay time</param>
        /// <param name="cancellation">Cancelation token</param>
        public AsyncTask(TContext context, Func<TContext, Task> action,
                         int delay = 0,
                         CancellationToken cancellation = default(CancellationToken))
                : this(context, action, delegate { return Task.FromResult(0); }, delay, cancellation)
        {
        }

        /// <summary>
        /// Creates <see cref="AsyncTask{TContext}"/>
        /// </summary>
        /// <param name="context"></param>
        /// <param name="action">Main action delegate</param>
        /// <param name="callback">Callback action delegate</param>
        /// <param name="delay">Delay time</param>
        /// <param name="cancellation">Cancelation token</param>
        public AsyncTask(TContext context, Func<TContext, Task> action,
                         Func<TContext, Task> callback,
                         int delay = 0,
                         CancellationToken cancellation = default(CancellationToken))
                : this(context, action, callback, delegate { return Task.FromResult(0); }, delay, cancellation)
        {
        }

        /// <summary>
        /// Creates <see cref="AsyncTask{TContext}"/>
        /// </summary>
        /// <param name="context"></param>
        /// <param name="action">Main action delegate</param>
        /// <param name="callback">Callback action delegate</param>
        /// <param name="errorCallback">Error callback action delegate</param>
        /// <param name="delay">Delay time</param>
        /// <param name="cancellation">Cancelation token</param>
        public AsyncTask(TContext context, Func<TContext, Task> action, 
                         Func<TContext, Task> callback, 
                         Func<TContext, Exception, Task> errorCallback,
                         int delay = 0,
                         CancellationToken cancellation = default(CancellationToken))
        {
            _action = action;
            _callBack = callback;
            _errorCallBack = errorCallback;

            Context = context;
            Delay = delay;
            Cancellation = cancellation;
        }

        #endregion

        public int Delay { get; }

        public CancellationToken Cancellation { get; }

        public TContext Context { get; }

        public async Task ExecuteAction()
        {
            await _action(Context);
        }

        public async Task ExecuteCallback()
        {
            await _callBack(Context);
        }

        public async Task ExecuteErrorCallback(Exception e)
        {
            await _errorCallBack(Context, e);
        }

        public override string ToString()
        {
            return Context.ToString();
        }
    }
}
