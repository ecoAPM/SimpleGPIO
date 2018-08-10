using System;
using SimpleGPIO.GPIO;
using SimpleGPIO.IO;
using SimpleGPIO.Power;

namespace SimpleGPIO.Tests.GPIO
{
    public class StubPinInterface : IPinInterface
    {
        public StubPinInterface(byte pin) => Pin = pin;

        public byte Pin { get; }

        public IOMode IOMode { get; set; }
        public Direction Direction { get; set; }

        public IPowerMode PowerMode { get; set; }
        public PowerValue Power { get; set; }
        public Voltage Voltage { get; set; }

        public void Enable() => throw new NotImplementedException();
        public void Disable() => throw new NotImplementedException();

        public void TurnOn()
        {
        }

        public void TurnOff()
        {
        }

        public void Spike()
        {
        }

        public void Toggle() => throw new NotImplementedException();
        public void Toggle(double hz, TimeSpan duration) => throw new NotImplementedException();
        public void Toggle(double hz, ulong iterations) => throw new NotImplementedException();

        public void OnPowerOn(Action action) => throw new NotImplementedException();
        public void OnPowerOff(Action action) => throw new NotImplementedException();
        public void OnPowerChange(Action action) => throw new NotImplementedException();

        public void Dispose()
        {
        }
    }
}