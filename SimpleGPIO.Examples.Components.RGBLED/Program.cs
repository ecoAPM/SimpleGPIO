using System;
using System.Threading.Tasks;
using SimpleGPIO.Boards;

namespace SimpleGPIO.Examples.Components.RGBLED
{
    public static class Program
    {
        public static async Task Main()
        {
            using var pi = new RaspberryPi();
            var led = new SimpleGPIO.Components.RGBLED(pi.GPIO23, pi.GPIO24, pi.GPIO25);
            var wait = TimeSpan.FromSeconds(0.5);

            led.TurnRed();
            await Task.Delay(wait);

            led.TurnYellow();
            await Task.Delay(wait);

            led.TurnGreen();
            await Task.Delay(wait);

            led.TurnCyan();
            await Task.Delay(wait);

            led.TurnBlue();
            await Task.Delay(wait);

            led.TurnPurple();
            await Task.Delay(wait);

            led.TurnOff();
            await Task.Delay(wait);

            led.TurnWhite();
            await Task.Delay(wait);
        }
    }
}
