using System.Device.Gpio;
using SimpleGPIO.Device;
using SimpleGPIO.IO;
using SimpleGPIO.Power;

namespace SimpleGPIO.GPIO;

public sealed class SystemPinInterface : PinInterface
{
	private readonly byte _pin;
	private readonly IGpioController _controller;

	public SystemPinInterface(byte bcmIdentifier, IGpioController controller)
	{
		_pin = bcmIdentifier;
		_controller = controller;
	}

	public override bool Enabled
	{
		get => _controller.IsPinOpen(_pin);
		set
		{
			if (value)
				_controller.OpenPin(_pin);
			else
				_controller.ClosePin(_pin);
		}
	}

	public override Direction Direction
	{
		get
		{
			if (!Enabled)
				Enable();

			return _controller.GetPinMode(_pin) == PinMode.Output ? Direction.Out : Direction.In;
		}
		set
		{
			if (!Enabled)
				Enable();

			_controller.SetPinMode(_pin, value == Direction.Out ? PinMode.Output : PinMode.Input);
		}
	}

	public override Voltage Voltage
	{
		get
		{
			if (!Enabled)
				Enable();

			return _controller.Read(_pin) == PinValue.High ? Voltage.High : Voltage.Low;
		}
		set
		{
			if (!Enabled)
				Enable();

			if (IOMode != IOMode.Write)
				IOMode = IOMode.Write;

			_controller.Write(_pin, value == Voltage.High ? PinValue.High : PinValue.Low);
		}
	}

	public override void OnPowerOn(Action action)
	{
		if (!Enabled)
			Enable();

		_controller.RegisterCallbackForPinValueChangedEvent(_pin, PinEventTypes.Rising, (_, _) => action());
	}

	public override void OnPowerOff(Action action)
	{
		if (!Enabled)
			Enable();

		_controller.RegisterCallbackForPinValueChangedEvent(_pin, PinEventTypes.Falling, (_, _) => action());
	}

	public override void OnPowerChange(Action action)
	{
		OnPowerOn(action);
		OnPowerOff(action);
	}
}