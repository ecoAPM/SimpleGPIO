using NSubstitute;
using SimpleGPIO.GPIO;
using SimpleGPIO.Tests.GPIO;
using Xunit;

namespace SimpleGPIO.Tests.Boards;

public sealed class BroadcomBoardTests
{
	[Fact]
	public void CanGetPinInterface()
	{
		//arrange
		var newPin = Substitute.For<Func<byte, IPinInterface>>();
		var board = new BroadcomStub(newPin);

		//act
		var pin0 = board.GPIO0;

		//assert
		Assert.IsAssignableFrom<IPinInterface>(pin0);
	}

	[Fact]
	public void PinInterfaceIsCached()
	{
		//arrange
		var newPin = Substitute.For<Func<byte, IPinInterface>>();
		var board = new BroadcomStub(newPin);
		var pin0 = board.GPIO0;

		//act
		var pin1 = board.GPIO0;

		//assert
		Assert.Equal(pin0, pin1);
		newPin.Received(1).Invoke(0);
	}

	[Fact]
	public void PinsAreMappedCorrectly()
	{
		//arrange
		var newPin = Substitute.For<Func<byte, IPinInterface>>();
		newPin.Invoke(Arg.Any<byte>()).Returns(p => new PinStub(p.Arg<byte>()));
		var board = new BroadcomStub(newPin);
		var pins = new PinStub[28];

		//act
		for (var x = 0; x < 28; x++)
			pins[x] = (PinStub)board.GetType().GetProperty($"GPIO{x}")?.GetValue(board)!;

		//assert
		for (var x = 0; x < 28; x++)
			Assert.Equal(x, pins[x].Pin);
	}

	[Fact]
	public void DisposesAllPins()
	{
		//arrange
		var newPin = Substitute.For<Func<byte, IPinInterface>>();
		var pin = Substitute.For<IPinInterface>();
		newPin.Invoke(Arg.Any<byte>()).Returns(_ => pin);
		var board = new BroadcomStub(newPin);
		for (var x = 0; x < 28; x++)
			board.GetType().GetProperty($"GPIO{x}")?.GetValue(board);

		//act
		board.Dispose();

		//assert
		pin.Received(28).Dispose();
	}
}