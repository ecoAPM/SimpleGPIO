using SimpleGPIO.GPIO;
using SimpleGPIO.Power;

namespace SimpleGPIO.SPI;

public abstract class Input
{
	private readonly IPinInterface _clock;
	private readonly IPinInterface _data;

	protected Input(IPinInterface data, IPinInterface clock)
	{
		_clock = clock;
		_data = data;
	}

	public void Send(PowerValue value)
	{
		_data.Power = value;
		_clock.Spike();
	}

	public void Send(IEnumerable<PowerValue> values)
	{
		foreach (var value in values)
		{
			Send(value);
		}
	}

	public void Send(byte value)
	{
		var values = new[]
		{
			GetBinaryDigitPowerValue(value, 7),
			GetBinaryDigitPowerValue(value, 6),
			GetBinaryDigitPowerValue(value, 5),
			GetBinaryDigitPowerValue(value, 4),
			GetBinaryDigitPowerValue(value, 3),
			GetBinaryDigitPowerValue(value, 2),
			GetBinaryDigitPowerValue(value, 1),
			GetBinaryDigitPowerValue(value, 0)
		};
		Send(values);
	}

	public void Send(IEnumerable<byte> values)
	{
		foreach (var value in values)
		{
			Send(value);
		}
	}

	private static PowerValue GetBinaryDigitPowerValue(byte value, byte digit)
		=> ((value >> digit) & 1) == 1
			? PowerValue.On
			: PowerValue.Off;
}