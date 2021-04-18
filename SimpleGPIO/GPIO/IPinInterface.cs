using System;
using System.Threading.Tasks;
using SimpleGPIO.IO;
using SimpleGPIO.Power;

namespace SimpleGPIO.GPIO
{
    public interface IPinInterface : IDisposable
    {
        bool Enabled { get; set; }
        IOMode IOMode { get; set; }
        Direction Direction { get; set; }
        
        IPowerMode PowerMode { get; set; }
        PowerValue Power { get; set; }
        Voltage Voltage { get; set; }

        void Enable();
        void Disable();
        
        void TurnOn();
        void TurnOff();
        void Spike();
        Task TurnOnFor(TimeSpan length);
        Task TurnOffFor(TimeSpan length);
        
        void Toggle();
        Task Toggle(double hz, TimeSpan duration);
        Task Toggle(double hz, ulong iterations);
        
        void OnPowerOn(Action action);
        void OnPowerOff(Action action);
        void OnPowerChange(Action action);
    }
}
