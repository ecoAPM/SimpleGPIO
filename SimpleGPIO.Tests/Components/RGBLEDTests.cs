using NSubstitute;
using SimpleGPIO.Components;
using SimpleGPIO.GPIO;
using SimpleGPIO.Power;
using SimpleGPIO.Tests.GPIO;
using Xunit;

namespace SimpleGPIO.Tests.Components
{
    public class RGBLEDTests
    {
        [Fact]
        public void TestRed()
        {
            //arrange
            var red = new StubPinInterface(1);
            var green = new StubPinInterface(2);
            var blue = new StubPinInterface(3);
            var led = new RGBLED(red, green, blue);

            //act
            led.TurnRed();

            //assert
            Assert.Equal(PowerValue.On, red.Power);
            Assert.Equal(PowerValue.Off, green.Power);
            Assert.Equal(PowerValue.Off, blue.Power);
        }

        [Fact]
        public void TestYellow()
        {
            //arrange
            var red = new StubPinInterface(1);
            var green = new StubPinInterface(2);
            var blue = new StubPinInterface(3);
            var led = new RGBLED(red, green, blue);

            //act
            led.TurnYellow();

            //assert
            Assert.Equal(PowerValue.On, red.Power);
            Assert.Equal(PowerValue.On, green.Power);
            Assert.Equal(PowerValue.Off, blue.Power);
        }

        [Fact]
        public void TestGreen()
        {
            //arrange
            var red = new StubPinInterface(1);
            var green = new StubPinInterface(2);
            var blue = new StubPinInterface(3);
            var led = new RGBLED(red, green, blue);

            //act
            led.TurnGreen();

            //assert
            Assert.Equal(PowerValue.Off, red.Power);
            Assert.Equal(PowerValue.On, green.Power);
            Assert.Equal(PowerValue.Off, blue.Power);
        }

        [Fact]
        public void TestCyan()
        {
            //arrange
            var red = new StubPinInterface(1);
            var green = new StubPinInterface(2);
            var blue = new StubPinInterface(3);
            var led = new RGBLED(red, green, blue);

            //act
            led.TurnCyan();

            //assert
            Assert.Equal(PowerValue.Off, red.Power);
            Assert.Equal(PowerValue.On, green.Power);
            Assert.Equal(PowerValue.On, blue.Power);
        }

        [Fact]
        public void TestBlue()
        {
            //arrange
            var red = new StubPinInterface(1);
            var green = new StubPinInterface(2);
            var blue = new StubPinInterface(3);
            var led = new RGBLED(red, green, blue);

            //act
            led.TurnBlue();

            //assert
            Assert.Equal(PowerValue.Off, red.Power);
            Assert.Equal(PowerValue.Off, green.Power);
            Assert.Equal(PowerValue.On, blue.Power);
        }

        [Fact]
        public void TestPurple()
        {
            //arrange
            var red = new StubPinInterface(1);
            var green = new StubPinInterface(2);
            var blue = new StubPinInterface(3);
            var led = new RGBLED(red, green, blue);

            //act
            led.TurnPurple();

            //assert
            Assert.Equal(PowerValue.On, red.Power);
            Assert.Equal(PowerValue.Off, green.Power);
            Assert.Equal(PowerValue.On, blue.Power);
        }

        [Fact]
        public void TestWhite()
        {
            //arrange
            var red = new StubPinInterface(1);
            var green = new StubPinInterface(2);
            var blue = new StubPinInterface(3);
            var led = new RGBLED(red, green, blue);

            //act
            led.TurnWhite();

            //assert
            Assert.Equal(PowerValue.On, red.Power);
            Assert.Equal(PowerValue.On, green.Power);
            Assert.Equal(PowerValue.On, blue.Power);
        }

        [Fact]
        public void TestOff()
        {
            //arrange
            var red = new StubPinInterface(1);
            var green = new StubPinInterface(2);
            var blue = new StubPinInterface(3);
            var led = new RGBLED(red, green, blue);

            //act
            led.TurnOff();

            //assert
            Assert.Equal(PowerValue.Off, red.Power);
            Assert.Equal(PowerValue.Off, green.Power);
            Assert.Equal(PowerValue.Off, blue.Power);
        }
    }
}
