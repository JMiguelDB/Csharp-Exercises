using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace AsynchronousProgrammingExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Run(async () =>
            {
                Coffee cup = Coffee.PourCoffee();
                Console.WriteLine("coffee is ready");
                var eggsTask = FryEggsAsync(2);
                var baconTask = FryBaconAsync(3);

                var allTasks = new List<Task> { eggsTask, baconTask };
                while (allTasks.Any())
                {
                    Task finished = await Task.WhenAny(allTasks);
                    if (finished == eggsTask)
                    {
                        Console.WriteLine("eggs are ready");
                    }
                    else if (finished == baconTask)
                    {
                        Console.WriteLine("bacon is ready");
                    }
                    allTasks.Remove(finished);
                }
                Console.WriteLine("Breakfast is ready!");
            }).GetAwaiter().GetResult();
            Console.ReadLine();
        }

        private static Task FryBaconAsync(int v)
        {
            Task task = Task.Run(() => Console.WriteLine("Frying {0} bacon...",v));
            return task;
        }

        private static Task FryEggsAsync(int v)
        {
            Task task = Task.Run(() => Console.WriteLine("Frying {0} eggs...", v));
            return task;
        }
    }

    class Coffee
    {
        public static Coffee PourCoffee()
        {
            Console.WriteLine("Making coffee...");
            Thread.Sleep(2000);
            return new Coffee();
        }
    }

    public class Pinger
    {
        private readonly Timer _timer;
        private readonly HttpClient _client;

        public Pinger(HttpClient client)
        {
            _client = new HttpClient();
            _timer = new Timer(Heartbeat, null, 1000, 1000);
        }

        public void Heartbeat(object state)
        {
            // Discard the result
            _ = DoAsyncPing();
        }

        private async Task DoAsyncPing()
        {
            await _client.GetAsync("http://mybackend/api/ping");
        }
    }

}
