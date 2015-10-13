using System;
using System.Threading.Tasks;

public class MyTask
{
    public static void Test()
    {
        const int Repetitions = 10000;

        Task task = Task.Run(() =>
            {
                for (int count = 0; count < Repetitions; count++)
                {
                    Console.Write("+");
                }
            });
        for (int count = 0; count < Repetitions; count++)
        {
            Console.Write("-");
        }
        task.Wait();// wait until the task completes;
    }
}