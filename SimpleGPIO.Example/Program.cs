using System;
using System.Threading.Tasks;
using SimpleGPIO.Boards;

namespace SimpleGPIO.Example
{
    internal static class Program
    {
        //GPIO on a Raspberry Pi made easy!
        private static async Task Main()
        {
            //first, instantiate a new board
            var pi = new RaspberryPi();

            //physical pin numbers are easier to determine
            var redLED = pi.Pin7;
            
            //helper methods abstract away the implementation details
            redLED.TurnOn();
            await Task.Delay(TimeSpan.FromSeconds(1));
            redLED.TurnOff();
            await Task.Delay(TimeSpan.FromSeconds(1));

            //you can also use the Broadcom identifiers
            var sameRedLED = pi.GPIO4;

            //now make the light flash!
            sameRedLED.Toggle(10, TimeSpan.FromSeconds(2));

            //always clean up your toys when you're done
            sameRedLED.TurnOff();
        }
    }
}