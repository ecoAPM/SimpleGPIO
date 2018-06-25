using System;
using System.Threading;
using SimpleGPIO.Boards;

namespace SimpleGPIO.Examples.Input
{
    public static class Program
    {
        public static void Main()
        {
            var pi = new RaspberryPi();
            var button = pi.GPIO17;
            while (true)
            {
                Console.WriteLine(button.Power);
                Thread.Sleep(50);
            }
        }
    }
}
