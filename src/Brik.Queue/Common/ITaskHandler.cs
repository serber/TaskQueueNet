using System.Threading.Tasks;

namespace Brik.Queue.Common
{
    /// <summary>
    /// <see cref="ITask"/> handler class
    /// </summary>
    public interface ITaskHandler
    {
        /// <summary>
        /// Handle <see cref="ITask"/> async
        /// </summary>
        /// <param name="task"><see cref="ITask"/> instance</param>
        Task HandleAsync(ITask task);
    }
}