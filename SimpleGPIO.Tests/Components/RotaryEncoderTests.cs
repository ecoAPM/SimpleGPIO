using SimpleGPIO.Components;
using SimpleGPIO.Tests.GPIO;
using Xunit;

namespace SimpleGPIO.Tests.Components
{
    public class RotaryEncoderTests
    {
        [Fact]
        public void OnIncreasePerformsActionWhenSet()
        {
            //arrange
            var increasePin = new StubPinInterface(1);
            var decreasePin = new StubPinInterface(2);
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
            var increasePin = new StubPinInterface(1);
            var decreasePin = new StubPinInterface(2);
            var dial = new RotaryEncoder(increasePin, decreasePin); 
            var called = false;
            dial.OnDecrease(() => called = true);

            //act
            decreasePin.Spike();

            //assert
            Assert.True(called);
        }
    }
}