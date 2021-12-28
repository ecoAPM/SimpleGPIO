using SimpleGPIO.GPIO;
using SimpleGPIO.Power;

namespace SimpleGPIO.Components;

/// <summary>A 595-style 8-bit shift register</summary>
public sealed class ShiftRegister : SPI.Input
{
	private readonly IPinInterface? _enabled;
	private readonly IPinInterface _output;
	private readonly IPinInterface? _clear;

	/// <summary>Creates a new shift register</summary>
	/// <param name="enabledPin">The pin controlling if the register is enabled</param>
	/// <param name="dataPin">The pin controlling the data input to the register</param>
	/// <param name="shiftPin">The pin controlling when to shift bits</param>
	/// <param name="outputPin">The pin controlling when to output data</param>
	/// <param name="clearPin">The pin controlling when to clear the register</param>
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

	/// <summary>Sets the value stored in the register</summary>
	/// <param name="value">The value to store</param>
	public void SetValue(byte value) => Send(value);

	/// <summary>Sets the power values stored in the register</summary>
	/// <param name="values">The values to store</param>
	public void SetPowerValues(PowerSet values)
	{
		_enabled?.TurnOn();
		Send(new[] { values.A, values.B, values.C, values.D, values.E, values.F, values.G, values.H });
		_output.Spike();
	}

	/// <summary>Clears the value stored in the register</summary>
	public void Clear()
	{
		_clear?.TurnOn();
		_output.Spike();
		_clear?.TurnOff();
	}

	/// <summary>The set of power values stored in the register</summary>
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