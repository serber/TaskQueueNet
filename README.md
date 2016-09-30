# Tasks queue
Implemets simple tasks queue

# Installation
```
PM> Install-Package TaskQueueNet
```

# Usage
```csharp
static void Main(string[] args)
{
    IQueueDispatcher<ITask> queueDispatcher = new QueueDispatcher(new ParallelTaskHandler());
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
```
