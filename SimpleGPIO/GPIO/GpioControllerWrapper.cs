using System.Device.Gpio;

namespace SimpleGPIO.GPIO;

public sealed class GpioControllerWrapper : IGpioController
{
	private readonly GpioController _controller = new();

	public bool IsPinOpen(byte pin) => _controller.IsPinOpen(pin);
	public void OpenPin(byte pin) => _controller.OpenPin(pin);
	public void ClosePin(byte pin) => _controller.ClosePin(pin);

	public PinMode GetPinMode(byte pin) => _controller.GetPinMode(pin);
	public void SetPinMode(byte pin, PinMode mode) => _controller.SetPinMode(pin, mode);

	public PinValue Read(byte pin) => _controller.Read(pin);
	public void Write(byte pin, PinValue value) => _controller.Write(pin, value);

	public void RegisterCallbackForPinValueChangedEvent(byte pin, PinEventTypes rising, PinChangeEventHandler e)
		=> _controller.RegisterCallbackForPinValueChangedEvent(pin, rising, e);

	public void Dispose() => _controller.Dispose();
}