using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Brik.Queue.Common;

namespace Brik.Queue
{
    public sealed class QueueDispatcher : IQueueDispatcher<ITask>
    {
        #region Pricate fields

        private bool _started;

        private readonly ConcurrentQueue<ITask> _tasksQueue;

        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1);

        private readonly ITaskHandler _taskHandler;

        #endregion

        #region C-tor

        public QueueDispatcher(ITaskHandler taskHandler)
        {
            _tasksQueue = new ConcurrentQueue<ITask>();
            _taskHandler = taskHandler;
        }

        #endregion

        #region Public methods

        public void Start()
        {
            _started = true;
            //---
            Task.Factory.StartNew(QueueAction);
        }

        public void Stop()
        {
            _started = false;
            //---
            while (!_tasksQueue.IsEmpty)
            {
                ITask task;
                _tasksQueue.TryDequeue(out task);
            }
        }

        public void Enqueue(ITask task)
        {
            if (!_started)
            {
                throw new Exception("Queue not started");
            }
            //---
            _tasksQueue.Enqueue(task);
            //---
            _semaphore.Release();
        }

        #endregion

        #region Private methods

        private async Task QueueAction()
        {
            while (_started)
            {
                await _semaphore.WaitAsync();
                //---
                ITask nextTask;
                if (_tasksQueue.TryDequeue(out nextTask))
                {
                    await _taskHandler.HandleAsync(nextTask);
                }
            }
        }

        #endregion
    }
}