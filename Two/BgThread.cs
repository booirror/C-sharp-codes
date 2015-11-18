using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadT
{
    class BgThread
    {
    public static void Test()
    {
        Thread t = new Thread(ThreadProc);
        //t.IsBackground = true;  // 加这一句
        t.Start();
 
        //t.Join();
    }
    static void ThreadProc(object state)
    {
        for(int i=0; i<10000; i++)
        {
            Console.WriteLine(".");
            Thread.Sleep(300);
        }
    }

    }
}
