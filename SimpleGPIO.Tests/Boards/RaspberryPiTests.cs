using System;
using NSubstitute;
using SimpleGPIO.Boards;
using SimpleGPIO.GPIO;
using SimpleGPIO.Tests.GPIO;
using Xunit;

namespace SimpleGPIO.Tests.Boards;

public class RaspberryPiTests
{
	// courtesy of the "pinout" python command
	// with some regex magic
	[Theory]
	[InlineData(3, 2)]
	[InlineData(5, 3)]
	[InlineData(7, 4)]
	[InlineData(8, 14)]
	[InlineData(10, 15)]
	[InlineData(11, 17)]
	[InlineData(12, 18)]
	[InlineData(13, 27)]
	[InlineData(15, 22)]
	[InlineData(16, 23)]
	[InlineData(18, 24)]
	[InlineData(19, 10)]
	[InlineData(21, 9)]
	[InlineData(22, 25)]
	[InlineData(23, 11)]
	[InlineData(24, 8)]
	[InlineData(26, 7)]
	[InlineData(27, 0)]
	[InlineData(28, 1)]
	[InlineData(29, 5)]
	[InlineData(31, 6)]
	[InlineData(32, 12)]
	[InlineData(33, 13)]
	[InlineData(35, 19)]
	[InlineData(36, 16)]
	[InlineData(37, 26)]
	[InlineData(38, 20)]
	[InlineData(40, 21)]
	public void PinsAreMappedCorrectly(byte physical, byte bcm)
	{
		//arrange
		var newPin = Substitute.For<Func<byte, IPinInterface>>();
		newPin.Invoke(Arg.Any<byte>()).Returns(p => new StubPinInterface(p.Arg<byte>()));
		var board = new RaspberryPi(newPin);

		//act
		var pin = (StubPinInterface)board.GetType().GetProperty($"Pin{physical}")?.GetValue(board);

		//assert
		Assert.Equal(bcm, pin?.Pin);
	}
}
