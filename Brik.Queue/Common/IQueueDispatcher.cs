namespace Brik.Queue.Common
{
    /// <summary>
    /// Queue dispather
    /// </summary>
    /// <typeparam name="TTask"></typeparam>
    public interface IQueueDispatcher<in TTask> where TTask : ITask
    {
        /// <summary>
        /// Starts queue dispatcher
        /// </summary>
        void Start();

        /// <summary>
        /// Stop queue dispatcher
        /// </summary>
        void Stop();

        /// <summary>
        /// Add task to queue
        /// </summary>
        /// <param name="task">Task</param>
        void Enqueue(TTask task);
    }
}