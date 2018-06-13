using System;
using SimpleGPIO.Properties;

namespace SimpleGPIO.GPIO
{
    public interface IPinInterface : IDisposable
    {
        IOMode IOMode { get; set; }
        Direction Direction { get; set; }
        Power Power { get; set; }
        Voltage Voltage { get; set; }

        void Enable();
        void Disable();
        
        void TurnOn();
        void TurnOff();
        void Toggle();

        void Toggle(double hz, TimeSpan duration);
        void Toggle(double hz, ulong iterations);
    }
}
