using SimpleGPIO.Components;
using SimpleGPIO.Tests.GPIO;
using Xunit;

namespace SimpleGPIO.Tests.Components;

public sealed class RotaryEncoderTests
{
	[Fact]
	public void OnIncreasePerformsActionWhenSet()
	{
		//arrange
		var increasePin = new PinStub(1);
		var decreasePin = new PinStub(2);
		var dial = new RotaryEncoder(increasePin, decreasePin);

		var called = false;
		dial.OnIncrease(() => called = true);

		//act
		increasePin.Spike();

		//assert
		Assert.True(called);
	}

	[Fact]
	public void OnDecreasePerformsActionWhenSet()
	{
		//arrange
		var increasePin = new PinStub(1);
		var decreasePin = new PinStub(2);
		var dial = new RotaryEncoder(increasePin, decreasePin);
		var called = false;
		dial.OnDecrease(() => called = true);

		//act
		decreasePin.Spike();

		//assert
		Assert.True(called);
	}
}