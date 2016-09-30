using Brik.Queue.Common;
using System.Threading.Tasks;

namespace Brik.Queue.ConsoleApp
{
    using System.Net.Http;
    using System.Threading;

    public class Program
    {
        public static void Main(string[] args)
        {
            IQueueDispatcher queueDispatcher = new QueueDispatcher(new ParallelTaskHandler());
            queueDispatcher.Start();
            //---
            queueDispatcher.Enqueue(new AsyncTask<string>("http://ya.ru", DownloadAction, 3000));
            queueDispatcher.Enqueue(new AsyncTask<string>("http://rambler.ru", DownloadAction));
            queueDispatcher.Enqueue(new AsyncTask<string>("http://mail.ru", DownloadAction));
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