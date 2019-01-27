using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace LearnCSharp
{
    class MyInterloced
    {
        private static int usingResource = 0;
        private const int numThreadIterations = 5;
        private const int numThreads = 10;


        static void Main(string[] args)
        {
            Thread myThread;
            Random rnd = new Random();
            for(int i=0;i<numThreads;i++)
            {
                myThread = new Thread(new ThreadStart(MyThreadProc));
                myThread.Name = String.Format("Thread{0}", i + 1);
                Thread.Sleep(rnd.Next(0, 1000));
                myThread.Start();
            }
            Console.ReadLine();
        }
        private static void MyThreadProc()
        {
            for(int i=0;i<numThreadIterations;i++)
            {
                UseResource();
                Thread.Sleep(1000);
            }
        }
        static bool UseResource()
        {
            if(0==Interlocked.Exchange(ref usingResource,1))
            {
                Console.WriteLine("{0} acquired the lock", Thread.CurrentThread.Name);
                Thread.Sleep(500);
                Console.WriteLine("{0} exiting lock", Thread.CurrentThread.Name);
                Interlocked.Exchange(ref usingResource, 0);
                return true;
            }
            else
            {
                Console.WriteLine("{0} was denied the lock", Thread.CurrentThread.Name);
                return false;
            }
        }
    }
}
