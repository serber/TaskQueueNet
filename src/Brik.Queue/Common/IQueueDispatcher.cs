namespace Brik.Queue.Common
{
    /// <summary>
    /// Queue dispather
    /// </summary>
    public interface IQueueDispatcher
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
        /// <param name="task"><see cref="ITask"/> instance</param>
        void Enqueue(ITask task);
    }
}