using SimpleGPIO.Boards;

namespace SimpleGPIO.Examples.CLI
{
    internal static class Program
    {
        private static void Main()
        {
            var pi = new RaspberryPi();
            var redLED = pi.Pin16;
            redLED.TurnOn();
        }
    }
}