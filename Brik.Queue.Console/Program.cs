using Brik.Queue.Common;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Brik.Queue.Console
{
    class Program
    {
        static void Main()
        {
            IQueueDispatcher<ITask> queueDispatcher = new QueueDispatcher(new ParallelTaskHandler());
            queueDispatcher.Start();
            //---
            queueDispatcher.Enqueue(new AsyncTask<string>("http://ya.ru", DownloadAction, 3000));
            queueDispatcher.Enqueue(new AsyncTask<string>("http://google.ru", DownloadAction));
            queueDispatcher.Enqueue(new AsyncTask<string>("http://rambler.ru", DownloadAction));
            //---
            System.Console.ReadKey();
        }

        private static async Task DownloadAction(string url)
        {
            System.Console.WriteLine($"Download started: {url}, Thread: {Thread.CurrentThread.ManagedThreadId}");

            await new HttpClient().GetStringAsync(url);

            System.Console.WriteLine($"Download complete: {url}, Thread: {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}