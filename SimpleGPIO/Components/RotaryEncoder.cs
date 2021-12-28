using SimpleGPIO.GPIO;
using SimpleGPIO.Power;

namespace SimpleGPIO.Components;

/// <summary>A rotary encoder input device</summary>
public sealed class RotaryEncoder
{
	private readonly IPinInterface _increase;
	private readonly IPinInterface _decrease;

	/// <summary>Creates a new rotary encoder</summary>
	/// <param name="clockPin">The pin connected to the "CLK" contact</param>
	/// <param name="dataPin">The pin connected to the "DT" contact</param>
	public RotaryEncoder(IPinInterface clockPin, IPinInterface dataPin)
	{
		_increase = clockPin;
		_decrease = dataPin;
	}

	/// <summary>Configures the action to run when the encoder is rotated clockwise</summary>
	/// <param name="action">The action to perform</param>
	public void OnIncrease(Action action)
	{
		_increase.OnPowerOn(() =>
		{
			if (_decrease.Power == PowerValue.Off)
				action();
		});
	}

	/// <summary>Configures the action to run when the encoder is rotated counter-clockwise</summary>
	/// <param name="action">The action to perform</param>
	public void OnDecrease(Action action)
	{
		_decrease.OnPowerOn(() =>
		{
			if (_increase.Power == PowerValue.Off)
				action();
		});
	}
}