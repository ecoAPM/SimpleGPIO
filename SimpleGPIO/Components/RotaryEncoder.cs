using System;
using SimpleGPIO.GPIO;
using SimpleGPIO.Power;

namespace SimpleGPIO.Components;

public class RotaryEncoder
{
	private readonly IPinInterface _increase;
	private readonly IPinInterface _decrease;

	public RotaryEncoder(IPinInterface clockPin, IPinInterface dataPin)
	{
		_increase = clockPin;
		_decrease = dataPin;
	}

	public void OnIncrease(Action action)
	{
		_increase.OnPowerOn(() =>
		{
			if (_decrease.Power == PowerValue.Off)
				action();
		});
	}

	public void OnDecrease(Action action) => _decrease.OnPowerOn(() =>
	{
		if (_increase.Power == PowerValue.Off)
			action();
	});
}
