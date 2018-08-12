using System;
using System.Threading;
using SimpleGPIO.Boards;

namespace SimpleGPIO.Examples.Components.BitShiftRegister
{
    public static class Program
    {
        public static void Main()
        {
            using var pi = new RaspberryPi();

            var register = new SimpleGPIO.Components.BitShiftRegister(pi.Pin13, pi.Pin11, pi.Pin15, pi.Pin16, pi.Pin18);
            for (byte x = 0; x < byte.MaxValue; x++)
            {
                Console.WriteLine(x);
                register.SetValue(x);
                Thread.Sleep(200);
            }

            Thread.Sleep(400);
            register.Clear();
        }
    }
}