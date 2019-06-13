using System;
using System.Runtime.Remoting.Contexts;
using System.Threading;

namespace SimpleMultiThreadApp
{
    [Synchronization]
    class Printer : ContextBoundObject
    {
        public void PrintNumbers(object state)
        {
            // Вывести информацию о потоке.
            Console.WriteLine("-> {0} is executing PrintNumbers()",
            Thread.CurrentThread.Name);
            // Вывести числа.
            Console.Write("Your numbers: ");
            for (int i = 0; i < 10; i++)
            {
                Random r = new Random();
                Thread.Sleep(500);
                Console.Write("{0}, ", i);
            }
            Console.WriteLine();
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Fun with the CLR Thread Pool *****\n");
            Console.WriteLine("Main thread started. ThreadID = {0}",
            Thread.CurrentThread.ManagedThreadId);
            Printer p = new Printer();
            WaitCallback workItem = new WaitCallback(p.PrintNumbers);
            // Поставить в очередь метод десять раз.
            for (int i = 0; i < 10; i++)
            {
                // Потоки из пула всегда являются фоновыми и имеют стандартный приоритет (ThreadPriority.Normal)
                ThreadPool.QueueUserWorkItem(workItem, "передаваемые данные");
            }

            Console.WriteLine("All tasks queued");
            Console.ReadLine();
        }
    }
}
