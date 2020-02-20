using System.Device.Gpio;

namespace SimpleGPIO.GPIO
{
    public interface IGpioController
    {
        bool IsPinOpen(byte pin);
        void OpenPin(byte pin);
        void ClosePin(byte pin);
        PinMode GetPinMode(byte pin);
        void SetPinMode(byte pin, PinMode mode);
        PinValue Read(byte pin);
        void Write(byte pin, PinValue value);
        void RegisterCallbackForPinValueChangedEvent(byte pin, PinEventTypes rising, PinChangeEventHandler e);
    }
}