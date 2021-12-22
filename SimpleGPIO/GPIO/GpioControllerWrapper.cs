using System.Device.Gpio;

namespace SimpleGPIO.GPIO;

public sealed class GpioControllerWrapper : IGpioController
{
	public GpioController Base { get; } = new();

	public bool IsPinOpen(byte pin) => Base.IsPinOpen(pin);
	public void OpenPin(byte pin) => Base.OpenPin(pin);
	public void ClosePin(byte pin) => Base.ClosePin(pin);

	public PinMode GetPinMode(byte pin) => Base.GetPinMode(pin);
	public void SetPinMode(byte pin, PinMode mode) => Base.SetPinMode(pin, mode);

	public PinValue Read(byte pin) => Base.Read(pin);
	public void Write(byte pin, PinValue value) => Base.Write(pin, value);

	public void RegisterCallbackForPinValueChangedEvent(byte pin, PinEventTypes rising, PinChangeEventHandler e)
		=> Base.RegisterCallbackForPinValueChangedEvent(pin, rising, e);

	public void Dispose() => Base.Dispose();
}