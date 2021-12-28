using System.Drawing;
using SimpleGPIO.GPIO;

namespace SimpleGPIO.Components;

public sealed class RGBLED
{
	private readonly IPinInterface _red;
	private readonly IPinInterface _green;
	private readonly IPinInterface _blue;

	public RGBLED(IPinInterface redPin, IPinInterface greenPin, IPinInterface bluePin)
	{
		_red = redPin;
		_green = greenPin;
		_blue = bluePin;
	}

	public void SetColor(Color color)
	{
		SetComponent(_red, color.R);
		SetComponent(_green, color.G);
		SetComponent(_blue, color.B);
	}

	private static void SetComponent(IPinInterface pin, byte value)
		=> pin.Strength = value / 2.55;

	public void TurnOff() => SetColor(Color.Black);

	public async Task FadeTo(Color color, TimeSpan duration)
		=> await Task.WhenAll(
			_red.FadeTo(color.R / 2.55, duration),
			_green.FadeTo(color.G / 2.55, duration),
			_blue.FadeTo(color.B / 2.55, duration)
		);

	public async Task FadeOut(TimeSpan duration)
		=> await FadeTo(Color.Black, duration);

	public async Task Pulse(Color color, TimeSpan duration)
	{
		SetColor(color);
		await FadeOut(duration);
	}
}