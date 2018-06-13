using System;
using NSubstitute;
using SimpleGPIO.Boards;
using SimpleGPIO.GPIO;
using SimpleGPIO.Tests.GPIO;
using Xunit;

namespace SimpleGPIO.Tests.Boards
{
    public class BroadcomBoardTests
    {
        [Fact]
        public void CanGetPinInterface()
        {
            //arrange
            var newPin = Substitute.For<Func<byte, IPinInterface>>();
            var board = new BroadcomBoard(newPin);

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
            var board = new BroadcomBoard(newPin);
            var pin0 = board.GPIO0;

            //act
            pin0 = board.GPIO0;

            //assert
            newPin.Received(1).Invoke(0);
        }

        [Fact]
        public void PinsAreMappedCorrectly()
        {
            //arrange
            var newPin = Substitute.For<Func<byte, IPinInterface>>();
            newPin.Invoke(Arg.Any<byte>()).Returns(p => new StubPinInterface(p.Arg<byte>()));
            var board = new BroadcomBoard(newPin);
            var pins = new StubPinInterface[28];

            //act
            for (var x = 0; x < 28; x++)
                pins[x] = (StubPinInterface)board.GetType().GetProperty($"GPIO{x}").GetValue(board);

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
            newPin.Invoke(Arg.Any<byte>()).Returns(p => pin);
            var board = new BroadcomBoard(newPin);
            for (var x = 0; x < 28; x++)
                board.GetType().GetProperty($"GPIO{x}").GetValue(board);

            //act
            board.Dispose();

            //assert
            pin.Received(28).Dispose();
        }
    }
}
