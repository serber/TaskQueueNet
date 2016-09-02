# Tasks queue
Implemets simple tasks queue

# Usage
```csharp
IQueueDispatcher<ITask> queueDispatcher = new QueueDispatcher(new TaskHandler());
queueDispatcher.Start();
//---
queueDispatcher.Enqueue(new SimpleTask(
    () =>
    {
        // Task action
        System.Console.WriteLine("Task started");
        //---
        throw new Exception("Some error");
    },
    () =>
    {
        // Task complete callback
        System.Console.WriteLine("Task completed");
    },
    exception =>
    {
        // Exception callback
        System.Console.WriteLine(exception.Message);
    }));
```
