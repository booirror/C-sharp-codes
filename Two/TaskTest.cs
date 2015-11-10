using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadTs
{
    class TaskTest
    {
        public static TimeSpan TestFlow()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            for (int i = 0; i < 10000; i++ )
            {
                Console.WriteLine("one {0}", i);
            }
            for (int i = 0; i < 10000; i++)
            {
                Console.WriteLine("two {0}", i);
            }
            watch.Stop();
            //Console.WriteLine("elapsed: {0}", watch.Elapsed);
            return watch.Elapsed;
        }

        public static TimeSpan TestWithTasks()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            Task[] tasks = 
            {
                Task.Run(()=>
                {
                    for (int i = 0; i < 10000; i++ )
                    {
                        Console.WriteLine("one {0}", i);
                    }
                }),
                Task.Run(()=>
                {
                    for (int i = 0; i < 10000; i++ )
                    {
                        Console.WriteLine("two {0}", i);
                    }
                })
            };
            Task.WaitAll(tasks);
            watch.Stop();
            //Console.WriteLine("elapsed: {0}", watch.Elapsed);
            return watch.Elapsed;
        }
    }
}
