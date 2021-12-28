using SimpleGPIO.GPIO;
using SimpleGPIO.Power;

namespace SimpleGPIO.Components;

public sealed class ShiftRegister : SPI.Input
{
	private readonly IPinInterface? _enabled;
	private readonly IPinInterface _output;
	private readonly IPinInterface? _clear;

	public ShiftRegister(IPinInterface enabledPin, IPinInterface dataPin, IPinInterface shiftPin, IPinInterface outputPin, IPinInterface? clearPin = null) : base(dataPin, shiftPin)
	{
		_enabled = enabledPin;
		_output = outputPin;
		_clear = clearPin;

		if (_enabled != null)
			_enabled.PowerMode = PowerMode.Differential;

		if (_clear != null)
			_clear.PowerMode = PowerMode.Differential;
	}

	public void SetValue(byte value) => Send(value);

	public void SetPowerValues(PowerSet values)
	{
		_enabled?.TurnOn();
		Send(new[] { values.A, values.B, values.C, values.D, values.E, values.F, values.G, values.H });
		_output.Spike();
	}

	public void Clear()
	{
		_clear?.TurnOn();
		_output.Spike();
		_clear?.TurnOff();
	}

	public sealed class PowerSet
	{
		public PowerValue A { get; init; }
		public PowerValue B { get; init; }
		public PowerValue C { get; init; }
		public PowerValue D { get; init; }
		public PowerValue E { get; init; }
		public PowerValue F { get; init; }
		public PowerValue G { get; init; }
		public PowerValue H { get; init; }
	}
}