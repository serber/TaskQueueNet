using System;
using Brik.Queue.Common;

namespace Brik.Queue.Console
{
    class Program
    {
        static void Main(string[] args)
        {
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
                }, () =>
                {
                    // Task complete callback
                    System.Console.WriteLine("Task completed");
                }, exception =>
                {
                    // Exception callback
                    System.Console.WriteLine("Task faild");
                }));
            //---
            System.Console.ReadKey();
        }
    }
}