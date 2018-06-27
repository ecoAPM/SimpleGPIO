using System;
using System.Threading.Tasks;
using SimpleGPIO.Boards;

namespace SimpleGPIO.Examples.CLI
{
    internal static class Program
    {
        private static async Task Main()
        {
            var pi = new RaspberryPi();
            var redLED = pi.Pin16;
            redLED.TurnOn();
            
            await Task.Delay(TimeSpan.FromSeconds(5));
            
            // I always pick up my playthings, and so
            // I will show you another good trick that I know
            pi.Dispose();
        }
    }
}