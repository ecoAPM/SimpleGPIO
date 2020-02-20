using System;
using System.Device.Gpio;
using System.Linq;
using NSubstitute;
using SimpleGPIO.GPIO;
using SimpleGPIO.IO;
using SimpleGPIO.Power;
using Xunit;

namespace SimpleGPIO.Tests.GPIO
{
    public class SystemPinInterfaceTests
    {
        [Fact]
        public void EnabledIfPinOpen()
        {
            //arrange
            var gpio = Substitute.For<IGpioController>();
            gpio.IsPinOpen(123).Returns(true);
            var pinInterface = new SystemPinInterface(123, gpio);

            //act
            var enabled = pinInterface.Enabled;

            //assert
            Assert.True(enabled);
        }

        [Fact]
        public void NotEnabledIfPinNotOpen()
        {
            //arrange
            var gpio = Substitute.For<IGpioController>();
            gpio.IsPinOpen(123).Returns(false);
            var pinInterface = new SystemPinInterface(123, gpio);

            //act
            var enabled = pinInterface.Enabled;

            //assert
            Assert.False(enabled);
        }

        [Theory]
        [InlineData(PinMode.Input, IOMode.Read)]
        [InlineData(PinMode.Output, IOMode.Write)]
        public void CanGetIOMode(PinMode value, IOMode expected)
        {
            //arrange
            var gpio = Substitute.For<IGpioController>();
            gpio.GetPinMode(123).Returns(value);

            var pinInterface = new SystemPinInterface(123, gpio);

            //act
            var io = pinInterface.IOMode;

            //assert
            Assert.Equal(expected, io);
        }

        [Fact]
        public void GettingIOModeEnablesPinIfNotEnabled()
        {
            //arrange
            var gpio = Substitute.For<IGpioController>();
            gpio.GetPinMode(123).Returns(PinMode.Input);
            var pinInterface = new SystemPinInterface(123, gpio);

            //act
            var io = pinInterface.IOMode;

            //assert
            gpio.Received().OpenPin(123);
        }

        [Theory]
        [InlineData(IOMode.Read, PinMode.Input)]
        [InlineData(IOMode.Write, PinMode.Output)]
        public void CanSetIOMode(IOMode io, PinMode expected)
        {
            //arrange
            var gpio = Substitute.For<IGpioController>();

            //act
            var pinInterface = new SystemPinInterface(123, gpio)
            {
                Enabled = true,
                IOMode = io
            };

            //assert
            gpio.Received().SetPinMode(123, expected);
        }

        [Fact]
        public void SettingIOModeEnablesPinIfNotEnabled()
        {
            //arrange
            var gpio = Substitute.For<IGpioController>();

            //act
            var pinInterface = new SystemPinInterface(123, gpio)
            {
                IOMode = IOMode.Write
            };

            //assert
            gpio.Received().OpenPin(123);
        }

        [Theory]
        [InlineData(typeof(Direct), 0, PowerValue.Off)]
        [InlineData(typeof(Direct), 1, PowerValue.On)]
        [InlineData(typeof(Differential), 1, PowerValue.Off)]
        [InlineData(typeof(Differential), 0, PowerValue.On)]
        public void CanGetPower(Type powerModeType, byte value, PowerValue expected)
        {
            //arrange
            var gpio = Substitute.For<IGpioController>();
            gpio.Read(123).Returns(value);

            var pinInterface = new SystemPinInterface(123, gpio)
            {
                Direction = Direction.Out,
                PowerMode = (IPowerMode)Activator.CreateInstance(powerModeType)
            };

            //act
            var power = pinInterface.Power;

            //assert
            Assert.Equal(expected, power);
        }

        [Fact]
        public void GettingPowerEnablesPinIfNotEnabled()
        {
            //arrange
            var gpio = Substitute.For<IGpioController>();
            gpio.Read(123).Returns(PinValue.High);
            var pinInterface = new SystemPinInterface(123, gpio)
            {
                Direction = Direction.In
            };

            //act
            var power = pinInterface.Power;

            //assert
            gpio.Received().OpenPin(123);
        }

        [Theory]
        [InlineData(typeof(Direct), PowerValue.Off, 0)]
        [InlineData(typeof(Direct), PowerValue.On, 1)]
        [InlineData(typeof(Differential), PowerValue.Off, 1)]
        [InlineData(typeof(Differential), PowerValue.On, 0)]
        public void CanSetPower(Type powerModeType, PowerValue power, byte expected)
        {
            //arrange
            var gpio = Substitute.For<IGpioController>();
            gpio.GetPinMode(123).Returns(PinMode.Output);

            //act
            var pinInterface = new SystemPinInterface(123, gpio)
            {
                Enabled = true,
                PowerMode = (IPowerMode)Activator.CreateInstance(powerModeType),
                Power = power
            };

            //assert
            gpio.Received().Write(123, expected);
        }

        [Fact]
        public void SettingPowerSetsIOModeIfNotWrite()
        {
            //arrange
            var gpio = Substitute.For<IGpioController>();
            gpio.GetPinMode(123).Returns(PinMode.Input);

            //act
            var pinInterface = new SystemPinInterface(123, gpio)
            {
                Power = PowerValue.On
            };

            //assert
            gpio.Received().SetPinMode(123, PinMode.Output);
        }

        [Fact]
        public void EnableOpensPin()
        {
            //arrange
            var gpio = Substitute.For<IGpioController>();

            var pinInterface = new SystemPinInterface(123, gpio)
            {
                Enabled = false
            };

            //act
            pinInterface.Enable();

            //assert
            gpio.Received().OpenPin(123);
        }

        [Fact]
        public void DisableSetsEnabledFalse()
        {
            //arrange
            var gpio = Substitute.For<IGpioController>();

            var pinInterface = new SystemPinInterface(123, gpio)
            {
                Enabled = true
            };

            //act
            pinInterface.Disable();

            //assert
            Assert.False(pinInterface.Enabled);
        }

        [Fact]
        public void TurnOnSetsPowerOn()
        {
            //arrange
            var gpio = Substitute.For<IGpioController>();
            gpio.GetPinMode(123).Returns(PinMode.Output);

            var pinInterface = new SystemPinInterface(123, gpio)
            {
                Power = PowerValue.Off
            };

            //act
            pinInterface.TurnOn();

            //assert
            gpio.Received().Write(123, PinValue.High);
        }

        [Fact]
        public void TurnOffSetsPowerOff()
        {
            //arrange
            var gpio = Substitute.For<IGpioController>();
            gpio.GetPinMode(123).Returns(PinMode.Output);

            var pinInterface = new SystemPinInterface(123, gpio)
            {
                Power = PowerValue.On
            };

            //act
            pinInterface.TurnOff();

            //assert
            gpio.Received().Write(123, PinValue.Low);
        }

        [Fact]
        public void SpikeTurnsOnThenOff()
        {
            //arrange
            var gpio = Substitute.For<IGpioController>();
            gpio.GetPinMode(123).Returns(PinMode.Output);

            var pinInterface = new SystemPinInterface(123, gpio);

            //act
            pinInterface.Spike();

            //assert
            var calls = gpio.ReceivedCalls().Where(c => c.GetMethodInfo().Name == "Write").ToArray();
            Assert.Equal(PinValue.High, calls[0].GetArguments()[1]);
            Assert.Equal(PinValue.Low, calls[1].GetArguments()[1]);
        }

        [Fact]
        public void TurnOnForTurnsOffAfter()
        {
            //arrange
            var gpio = Substitute.For<IGpioController>();
            gpio.GetPinMode(123).Returns(PinMode.Output);

            var pinInterface = new SystemPinInterface(123, gpio);

            //act
            pinInterface.TurnOnFor(TimeSpan.Zero);

            //assert
            var calls = gpio.ReceivedCalls().Where(c => c.GetMethodInfo().Name == "Write").ToArray();
            Assert.Equal(PinValue.High, calls[0].GetArguments()[1]);
            Assert.Equal(PinValue.Low, calls[1].GetArguments()[1]);
        }

        [Fact]
        public void TurnOffForTurnsOnAfter()
        {
            //arrange
            var gpio = Substitute.For<IGpioController>();
            gpio.GetPinMode(123).Returns(PinMode.Output);

            var pinInterface = new SystemPinInterface(123, gpio);

            //act
            pinInterface.TurnOffFor(TimeSpan.Zero);

            //assert
            var calls = gpio.ReceivedCalls().Where(c => c.GetMethodInfo().Name == "Write").ToArray();
            Assert.Equal(PinValue.Low, calls[0].GetArguments()[1]);
            Assert.Equal(PinValue.High, calls[1].GetArguments()[1]);
        }

        [Fact]
        public void ToggleTurnsOnIfOff()
        {
            //arrange
            var gpio = Substitute.For<IGpioController>();
            gpio.GetPinMode(123).Returns(PinMode.Output);
            gpio.Read(123).Returns(PinValue.Low);

            var pinInterface = new SystemPinInterface(123, gpio)
            {
                Power = PowerValue.Off
            };

            //act
            pinInterface.Toggle();

            //assert
            gpio.Received().Write(123, PinValue.High);
        }

        [Fact]
        public void ToggleTurnsOffIfOn()
        {
            //arrange
            var gpio = Substitute.For<IGpioController>();
            gpio.GetPinMode(123).Returns(PinMode.Output);
            gpio.Read(123).Returns(PinValue.High);

            var pinInterface = new SystemPinInterface(123, gpio)
            {
                Power = PowerValue.On
            };

            //act
            pinInterface.Toggle();

            //assert
            gpio.Received().Write(123, PinValue.Low);
        }

        [Fact]
        public void CanToggleForADuration()
        {
            //arrange
            var gpio = Substitute.For<IGpioController>();
            gpio.GetPinMode(123).Returns(PinMode.Output);
            gpio.Read(123).Returns(PinValue.Low);

            var pinInterface = new SystemPinInterface(123, gpio);

            //act
            pinInterface.Toggle(1000, TimeSpan.FromMilliseconds(1));

            //assert
            gpio.Received(2).Write(123, Arg.Any<PinValue>());
        }

        [Fact]
        public void CanToggleForSetIterations()
        {
            //arrange
            var gpio = Substitute.For<IGpioController>();
            gpio.GetPinMode(123).Returns(PinMode.Output);
            gpio.Read(123).Returns(PinValue.Low);

            var pinInterface = new SystemPinInterface(123, gpio);

            //act
            pinInterface.Toggle(TimeSpan.TicksPerMillisecond, 10);

            //assert
            gpio.Received(20).Write(123, Arg.Any<PinValue>());
        }

        [Fact]
        public void OnPowerOnEnablesIfNotEnabled()
        {
            //arrange
            var gpio = Substitute.For<IGpioController>();
            gpio.GetPinMode(123).Returns(PinMode.Input);

            var pinInterface = new SystemPinInterface(123, gpio);

            //act
            pinInterface.OnPowerOn(null);

            //assert
            gpio.Received().OpenPin(123);
        }

        [Fact]
        public void OnPowerOnPerformsAction()
        {
            //arrange
            var gpio = Substitute.For<IGpioController>();
            gpio.GetPinMode(123).Returns(PinMode.Input);
            gpio.When(g => g.RegisterCallbackForPinValueChangedEvent(123, PinEventTypes.Rising, Arg.Any<PinChangeEventHandler>()))
                .Do(c => c.Arg<PinChangeEventHandler>().Invoke(null, null));

            var pinInterface = new SystemPinInterface(123, gpio)
            {
                Power = PowerValue.On
            };
            var called = false;

            //act
            pinInterface.OnPowerOn(() => called = true);

            //assert
            Assert.True(called);
        }

        [Fact]
        public void OnPowerOffEnablesIfNotEnabled()
        {
            //arrange
            var gpio = Substitute.For<IGpioController>();
            gpio.GetPinMode(123).Returns(PinMode.Input);

            var pinInterface = new SystemPinInterface(123, gpio);

            //act
            pinInterface.OnPowerOff(null);

            //assert
            gpio.Received().OpenPin(123);
        }

        [Fact]
        public void OnPowerOffPerformsAction()
        {
            //arrange
            var gpio = Substitute.For<IGpioController>();
            gpio.GetPinMode(123).Returns(PinMode.Input);
            gpio.When(g => g.RegisterCallbackForPinValueChangedEvent(123, PinEventTypes.Falling, Arg.Any<PinChangeEventHandler>()))
                .Do(c => c.Arg<PinChangeEventHandler>().Invoke(null, null));


            var pinInterface = new SystemPinInterface(123, gpio)
            {
                Power = PowerValue.Off
            };
            var called = false;

            //act
            pinInterface.OnPowerOff(() => called = true);

            //assert
            Assert.True(called);
        }

        [Fact]
        public void OnPowerChangeEnablesIfNotEnabled()
        {
            //arrange
            var gpio = Substitute.For<IGpioController>();
            gpio.GetPinMode(123).Returns(PinMode.Input);

            var pinInterface = new SystemPinInterface(123, gpio);

            //act
            pinInterface.OnPowerChange(null);

            //assert
            gpio.Received().OpenPin(123);
        }

        [Fact]
        public void OnPowerChangePerformsAction()
        {
            //arrange
            var gpio = Substitute.For<IGpioController>();
            gpio.GetPinMode(123).Returns(PinMode.Input);
            gpio.When(g => g.RegisterCallbackForPinValueChangedEvent(123, Arg.Any<PinEventTypes>(), Arg.Any<PinChangeEventHandler>()))
                .Do(c => c.Arg<PinChangeEventHandler>().Invoke(null, null));
            var pinInterface = new SystemPinInterface(123, gpio);
            var called = false;

            //act
            pinInterface.OnPowerChange(() => called = true);

            //assert
            Assert.True(called);
        }

        [Fact]
        public void DisablesOnDispose()
        {
            //arrange
            var gpio = Substitute.For<IGpioController>();
            gpio.GetPinMode(123).Returns(PinMode.Output);

            var pinInterface = new SystemPinInterface(123, gpio);

            //act
            pinInterface.Dispose();

            //assert
            gpio.Received().ClosePin(123);
        }
    }
}
