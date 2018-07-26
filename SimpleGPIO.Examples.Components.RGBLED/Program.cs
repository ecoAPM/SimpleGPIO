using System;
using System.Threading;
using SimpleGPIO.Boards;

namespace SimpleGPIO.Examples.Components.RGBLED
{
    public static class Program
    {
        public static void Main()
        {
            using (var pi = new RaspberryPi())
            {
                var led = new SimpleGPIO.Components.RGBLED(pi.GPIO23, pi.GPIO24, pi.GPIO25);
                var wait = TimeSpan.FromSeconds(0.5);

                led.TurnRed();
                Thread.Sleep(wait);

                led.TurnYellow();
                Thread.Sleep(wait);

                led.TurnGreen();
                Thread.Sleep(wait);

                led.TurnCyan();
                Thread.Sleep(wait);

                led.TurnBlue();
                Thread.Sleep(wait);

                led.TurnPurple();
                Thread.Sleep(wait);

                led.TurnOff();
                Thread.Sleep(wait);

                led.TurnWhite();
                Thread.Sleep(wait);
            }
        }
    }
}
