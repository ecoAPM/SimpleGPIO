using System;
using System.Threading;
using SimpleGPIO.Boards;

namespace SimpleGPIO.Example
{
    internal static class Program
    {
        private static void Main()
        {
            var pi = new RaspberryPi();

            var redLED = pi.Pin16;
            var yellowLED = pi.Pin18;
            var greenLED = pi.Pin22;
            
            redLED.TurnOn();
            Thread.Sleep(TimeSpan.FromSeconds(1));
            redLED.TurnOff();

            yellowLED.TurnOn();
            Thread.Sleep(TimeSpan.FromSeconds(1));
            yellowLED.TurnOff();

            greenLED.TurnOn();
            Thread.Sleep(TimeSpan.FromSeconds(1));
            greenLED.TurnOff();

            pi.Dispose();
        }
    }
}