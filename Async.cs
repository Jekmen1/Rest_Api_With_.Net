using System;
using System.Threading;
using System.Threading.Tasks;

namespace Helloworld
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            Task firstTask = new Task(() =>
            {
                Thread.Sleep(100);
                Console.WriteLine("Task 1");
            });
            firstTask.Start();

            Task secondTask = ConsoleAfterDelayAsync("Task 2", 150);

            ConsoleAfterDelay("delay", 101);

            Task thirdTask = ConsoleAfterDelayAsync("Task 3", 50); 

            await secondTask;
            await Task.WhenAll(firstTask);
            Console.WriteLine("After all tasks were created");
            await thirdTask;
        }

        static void ConsoleAfterDelay(string text, int delayTime)
        {
            Thread.Sleep(delayTime);
            Console.WriteLine(text);
        }

        static async Task ConsoleAfterDelayAsync(string text, int delayTime)
        {
            await Task.Delay(delayTime);
            Console.WriteLine(text);
        }
    }
}
