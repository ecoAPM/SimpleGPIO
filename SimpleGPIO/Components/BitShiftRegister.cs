using SimpleGPIO.GPIO;
using SimpleGPIO.Power;

namespace SimpleGPIO.Components;

public sealed class BitShiftRegister
{
	private readonly IPinInterface? _enabled;
	private readonly IPinInterface _data;
	private readonly IPinInterface _shift;
	private readonly IPinInterface _output;
	private readonly IPinInterface? _clear;

	public BitShiftRegister(IPinInterface enabledPin, IPinInterface dataPin, IPinInterface shiftPin, IPinInterface outputPin, IPinInterface? clearPin = null)
	{
		_enabled = enabledPin;
		_data = dataPin;
		_shift = shiftPin;
		_output = outputPin;
		_clear = clearPin;

		if (_enabled != null)
			_enabled.PowerMode = PowerMode.Differential;

		if (_clear != null)
			_clear.PowerMode = PowerMode.Differential;
	}

	public void SetValue(byte value)
	{
		SetPowerValues(new PowerSet
		{
			A = GetBinaryDigitPowerValue(value, 7),
			B = GetBinaryDigitPowerValue(value, 6),
			C = GetBinaryDigitPowerValue(value, 5),
			D = GetBinaryDigitPowerValue(value, 4),
			E = GetBinaryDigitPowerValue(value, 3),
			F = GetBinaryDigitPowerValue(value, 2),
			G = GetBinaryDigitPowerValue(value, 1),
			H = GetBinaryDigitPowerValue(value, 0)
		});
	}

	public static PowerValue GetBinaryDigitPowerValue(byte value, byte digit)
		=> ((value >> digit) & 1) == 1
			? PowerValue.On
			: PowerValue.Off;

	public void SetPowerValues(PowerSet values)
	{
		_enabled?.TurnOn();

		_data.Power = values.A;
		_shift.Spike();

		_data.Power = values.B;
		_shift.Spike();

		_data.Power = values.C;
		_shift.Spike();

		_data.Power = values.D;
		_shift.Spike();

		_data.Power = values.E;
		_shift.Spike();

		_data.Power = values.F;
		_shift.Spike();

		_data.Power = values.G;
		_shift.Spike();

		_data.Power = values.H;
		_shift.Spike();

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