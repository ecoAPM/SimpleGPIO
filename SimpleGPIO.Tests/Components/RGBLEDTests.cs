using NSubstitute;
using SimpleGPIO.Components;
using SimpleGPIO.GPIO;
using Xunit;

namespace SimpleGPIO.Tests.Components
{
    public class RGBLEDTests
    {
        [Fact]
        public void TestRed()
        {
            //arrange
            var red = Substitute.For<IPinInterface>();
            var green = Substitute.For<IPinInterface>();
            var blue = Substitute.For<IPinInterface>();
            var led = new RGBLED(red, green, blue);

            //act
            led.TurnRed();

            //assert
            red.Received().TurnOn();
            green.Received().TurnOff();
            blue.Received().TurnOff();
        }

        [Fact]
        public void TestYellow()
        {
            //arrange
            var red = Substitute.For<IPinInterface>();
            var green = Substitute.For<IPinInterface>();
            var blue = Substitute.For<IPinInterface>();
            var led = new RGBLED(red, green, blue);

            //act
            led.TurnYellow();

            //assert
            red.Received().TurnOn();
            green.Received().TurnOn();
            blue.Received().TurnOff();
        }

        [Fact]
        public void TestGreen()
        {
            //arrange
            var red = Substitute.For<IPinInterface>();
            var green = Substitute.For<IPinInterface>();
            var blue = Substitute.For<IPinInterface>();
            var led = new RGBLED(red, green, blue);

            //act
            led.TurnGreen();

            //assert
            red.Received().TurnOff();
            green.Received().TurnOn();
            blue.Received().TurnOff();
        }

        [Fact]
        public void TestCyan()
        {
            //arrange
            var red = Substitute.For<IPinInterface>();
            var green = Substitute.For<IPinInterface>();
            var blue = Substitute.For<IPinInterface>();
            var led = new RGBLED(red, green, blue);

            //act
            led.TurnCyan();

            //assert
            red.Received().TurnOff();
            green.Received().TurnOn();
            blue.Received().TurnOn();
        }

        [Fact]
        public void TestBlue()
        {
            //arrange
            var red = Substitute.For<IPinInterface>();
            var green = Substitute.For<IPinInterface>();
            var blue = Substitute.For<IPinInterface>();
            var led = new RGBLED(red, green, blue);

            //act
            led.TurnBlue();

            //assert
            red.Received().TurnOff();
            green.Received().TurnOff();
            blue.Received().TurnOn();
        }

        [Fact]
        public void TestPurple()
        {
            //arrange
            var red = Substitute.For<IPinInterface>();
            var green = Substitute.For<IPinInterface>();
            var blue = Substitute.For<IPinInterface>();
            var led = new RGBLED(red, green, blue);

            //act
            led.TurnPurple();

            //assert
            red.Received().TurnOn();
            green.Received().TurnOff();
            blue.Received().TurnOn();
        }

        [Fact]
        public void TestWhite()
        {
            //arrange
            var red = Substitute.For<IPinInterface>();
            var green = Substitute.For<IPinInterface>();
            var blue = Substitute.For<IPinInterface>();
            var led = new RGBLED(red, green, blue);

            //act
            led.TurnWhite();

            //assert
            red.Received().TurnOn();
            green.Received().TurnOn();
            blue.Received().TurnOn();
        }

        [Fact]
        public void TestOff()
        {
            //arrange
            var red = Substitute.For<IPinInterface>();
            var green = Substitute.For<IPinInterface>();
            var blue = Substitute.For<IPinInterface>();
            var led = new RGBLED(red, green, blue);

            //act
            led.TurnOff();

            //assert
            red.Received().TurnOff();
            green.Received().TurnOff();
            blue.Received().TurnOff();
        }
    }
}
