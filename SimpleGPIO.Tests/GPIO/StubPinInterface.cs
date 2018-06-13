using System;
using SimpleGPIO.GPIO;
using SimpleGPIO.Properties;

namespace SimpleGPIO.Tests.GPIO
{
    public class StubPinInterface : IPinInterface
    {
        public StubPinInterface(byte pin) => Pin = pin;

        public byte Pin { get; }
        public IOMode IOMode { get; set; }
        public Direction Direction { get; set; }
        public Power Power { get; set; }
        public Voltage Voltage { get; set; }

        public void Enable() => throw new NotImplementedException();
        public void Disable() => throw new NotImplementedException();
        public void TurnOn() => throw new NotImplementedException();
        public void TurnOff() => throw new NotImplementedException();
        public void Toggle() => throw new NotImplementedException();
        public void Toggle(double hz, TimeSpan duration) => throw new NotImplementedException();
        public void Toggle(double hz, ulong iterations) => throw new NotImplementedException();

        public void Dispose()
        {
        }
    }
}