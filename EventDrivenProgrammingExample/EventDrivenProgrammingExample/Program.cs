using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace EventDrivenProgrammingExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Timer myTimer = new Timer(2000);
            //Suscribing events
            myTimer.Elapsed += MyTimer_Elapsed;
            myTimer.Elapsed += MyTimer_Elapsed1;
            
            myTimer.Start();
            //Unsuscribing events
            Console.WriteLine("Press enter to remove the red event.");
            Console.ReadLine();
            myTimer.Elapsed -= MyTimer_Elapsed1;

            Console.ReadLine();
        }

        private static void MyTimer_Elapsed(object sender, ElapsedEventArgs elapsedEvent)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Elapsed: {0:HH:mm:ss.fff}",elapsedEvent.SignalTime);
        }

        private static void MyTimer_Elapsed1(object sender, ElapsedEventArgs elapsedEvent)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Elapsed: {0:HH:mm:ss.fff}", elapsedEvent.SignalTime);
        }
    }
}
