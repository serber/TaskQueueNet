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
            queueDispatcher.Enqueue(new SimpleTask<int>(1234567890, IntAction));
            queueDispatcher.Enqueue(new SimpleTask<string>("Some string", StringAction));
            queueDispatcher.Enqueue(new SimpleTask(VoidAction));
            //---
            System.Console.ReadKey();
        }

        private static void VoidAction()
        {
            System.Console.WriteLine($"VoidAction");
        }

        private static void StringAction(string str)
        {
            System.Console.WriteLine($"StringAction: {str}");
        }

        private static void IntAction(int i)
        {
            System.Console.WriteLine($"IntAction: {i}");
        }
    }
}