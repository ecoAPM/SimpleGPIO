using System.Collections;
using System.Drawing;
using SimpleGPIO.Components;
using SimpleGPIO.Power;
using SimpleGPIO.Tests.GPIO;
using Xunit;

namespace SimpleGPIO.Tests.Components;

public sealed class RGBLEDTests
{
	[Theory]
	[ClassData(typeof(ColorData))]
	public void ColorsMatch(Color color, double r, double g, double b)
	{
		//arrange
		var red = new PinStub(1);
		var green = new PinStub(2);
		var blue = new PinStub(3);
		var led = new RGBLED(red, green, blue);

		//act
		led.SetColor(color);

		//assert
		Assert.Equal(r, red.Strength);
		Assert.Equal(g, green.Strength);
		Assert.Equal(b, blue.Strength);

		Assert.Equal(r > 0 ? PowerValue.On : PowerValue.Off, red.Power);
		Assert.Equal(g > 0 ? PowerValue.On : PowerValue.Off, green.Power);
		Assert.Equal(b > 0 ? PowerValue.On : PowerValue.Off, blue.Power);
	}

	[Fact]
	public void TurnOffTurnsOffAllPins()
	{
		//arrange
		var red = new PinStub(1);
		var green = new PinStub(2);
		var blue = new PinStub(3);
		var led = new RGBLED(red, green, blue);

		//act
		led.TurnOff();

		//assert
		Assert.Equal(PowerValue.Off, red.Power);
		Assert.Equal(PowerValue.Off, green.Power);
		Assert.Equal(PowerValue.Off, blue.Power);
	}

	[Fact]
	public async Task CanFadeToColor()
	{
		//arrange
		var red = new PinStub(1);
		var green = new PinStub(2);
		var blue = new PinStub(3);
		var led = new RGBLED(red, green, blue);

		//act
		var ecoGreen = Color.FromArgb(83, 141, 67);
		await led.FadeTo(ecoGreen, TimeSpan.Zero);

		//assert
		Assert.Equal(32.5, red.Strength, 1);
		Assert.Equal(55.3, green.Strength, 1);
		Assert.Equal(26.3, blue.Strength, 1);
	}

	[Fact]
	public async Task CanFadeOutToBlack()
	{
		//arrange
		var red = new PinStub(1);
		var green = new PinStub(2);
		var blue = new PinStub(3);
		var led = new RGBLED(red, green, blue);

		//act
		await led.FadeOut(TimeSpan.Zero);

		//assert
		Assert.Equal(PowerValue.Off, red.Power);
		Assert.Equal(PowerValue.Off, green.Power);
		Assert.Equal(PowerValue.Off, blue.Power);
	}

	[Fact]
	public async Task PulseFadesOutCompletely()
	{
		//arrange
		var red = new PinStub(1);
		var green = new PinStub(2);
		var blue = new PinStub(3);
		var led = new RGBLED(red, green, blue);

		//act
		await led.Pulse(Color.White, TimeSpan.Zero);

		//assert
		Assert.Equal(PowerValue.Off, red.Power);
		Assert.Equal(PowerValue.Off, green.Power);
		Assert.Equal(PowerValue.Off, blue.Power);
	}

	private sealed class ColorData : IEnumerable<object[]>
	{
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		public IEnumerator<object[]> GetEnumerator()
		{
			yield return new object[] { Color.Red, 100, 0, 0 };
			yield return new object[] { Color.Yellow, 100, 100, 0 };
			yield return new object[] { Color.Lime, 0, 100, 0 };
			yield return new object[] { Color.Cyan, 0, 100, 100 };
			yield return new object[] { Color.Blue, 0, 0, 100 };
			yield return new object[] { Color.Magenta, 100, 0, 100 };
			yield return new object[] { Color.White, 100, 100, 100 };
			yield return new object[] { Color.Black, 0, 0, 0 };
		}
	}
}