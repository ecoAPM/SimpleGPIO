using System;
using System.Threading;
using SimpleGPIO.Boards;

namespace SimpleGPIO.Examples.Components.Motor
{
    public static class Program
    {
        public static void Main()
        {
            using (var pi = new RaspberryPi())
            {
                var enabledPin = pi.Pin11;
                var clockwisePin = pi.Pin13;
                var counterclockwisePin = pi.Pin15;
                var motor = new SimpleGPIO.Components.Motor(enabledPin, clockwisePin, counterclockwisePin);

                motor.TurnClockwiseFor(TimeSpan.FromSeconds(2));
                //give it a second to fully stop before reversing
                Thread.Sleep(TimeSpan.FromSeconds(1));

                motor.TurnCounterclockwiseFor(TimeSpan.FromSeconds(1), true);
                //give it some cooldown time before disposing,
                //as counterclockwisePin turning off will abruptly stop the motor
                Thread.Sleep(TimeSpan.FromSeconds(2));
            }
        }
    }
}
