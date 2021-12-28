using System.Drawing;
using SimpleGPIO.GPIO;

namespace SimpleGPIO.Components;

/// <summary>An LED with separate diodes for red, green, and blue</summary>
public sealed class RGBLED
{
	private readonly IPinInterface _red;
	private readonly IPinInterface _green;
	private readonly IPinInterface _blue;

	/// <summary>Creates a new RGB LED</summary>
	/// <param name="redPin">The pin controlling the red diode</param>
	/// <param name="greenPin">The pin controlling the green diode</param>
	/// <param name="bluePin">The pin controlling the blue diode</param>
	public RGBLED(IPinInterface redPin, IPinInterface greenPin, IPinInterface bluePin)
	{
		_red = redPin;
		_green = greenPin;
		_blue = bluePin;
	}

	/// <summary>Sets the LED to the specified color</summary>
	/// <param name="color">The color to make the LED</param>
	public void SetColor(Color color)
	{
		SetComponent(_red, color.R);
		SetComponent(_green, color.G);
		SetComponent(_blue, color.B);
	}

	private static void SetComponent(IPinInterface pin, byte value)
		=> pin.Strength = value / 2.55;

	/// <summary>Turns the LED off</summary>
	public void TurnOff() => SetColor(Color.Black);

	/// <summary>Fades the LED to the specified color over a given duration</summary>
	/// <param name="color">The color to fade to</param>
	/// <param name="duration">The duration to fade over</param>
	public async Task FadeTo(Color color, TimeSpan duration)
		=> await Task.WhenAll(
			_red.FadeTo(color.R / 2.55, duration),
			_green.FadeTo(color.G / 2.55, duration),
			_blue.FadeTo(color.B / 2.55, duration)
		);

	/// <summary>Fades the LED out to black/off</summary>
	/// <param name="duration">The duration to fade over</param>
	public async Task FadeOut(TimeSpan duration)
		=> await FadeTo(Color.Black, duration);

	/// <summary>Sets the LED to the specified color, and then fades out over a given duration</summary>
	/// <param name="color">The color to fade to</param>
	/// <param name="duration">The duration to fade over</param>
	public async Task Pulse(Color color, TimeSpan duration)
	{
		SetColor(color);
		await FadeOut(duration);
	}
}