using System;
using SimpleGPIO.Boards;

namespace SimpleGPIO.Examples.Components.RotaryEncoder
{
    public static class Program
    {
        public static void Main()
        {
            using(var pi = new RaspberryPi())
            {
                var dial = new SimpleGPIO.Components.RotaryEncoder(pi.Pin11, pi.Pin13);
        
                var x = 0;
                dial.OnIncrease(() => Console.WriteLine(++x));
                dial.OnDecrease(() => Console.WriteLine(--x));
                
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}
