using System.Device.Gpio;
using NSubstitute;
using SimpleGPIO.Device;
using SimpleGPIO.GPIO;
using SimpleGPIO.IO;
using SimpleGPIO.Power;
using Xunit;

namespace SimpleGPIO.Tests.GPIO;

public sealed class SystemPinInterfaceTests
{
	[Fact]
	public void EnabledIfPinOpen()
	{
		//arrange
		using var gpio = Substitute.For<IGpioController>();
		gpio.IsPinOpen(123).Returns(true);
		using var pwm = Substitute.For<IPwmChannel>();
		using var pinInterface = new SystemPinInterface(123, gpio, pwm);

		//act
		var enabled = pinInterface.Enabled;

		//assert
		Assert.True(enabled);
	}

	[Fact]
	public void NotEnabledIfPinNotOpen()
	{
		//arrange
		using var gpio = Substitute.For<IGpioController>();
		gpio.IsPinOpen(123).Returns(false);
		using var pwm = Substitute.For<IPwmChannel>();
		using var pinInterface = new SystemPinInterface(123, gpio, pwm);

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
		using var gpio = Substitute.For<IGpioController>();
		gpio.GetPinMode(123).Returns(value);
		using var pwm = Substitute.For<IPwmChannel>();
		using var pinInterface = new SystemPinInterface(123, gpio, pwm);

		//act
		var io = pinInterface.IOMode;

		//assert
		Assert.Equal(expected, io);
	}

	[Fact]
	public void GettingIOModeEnablesPinIfNotEnabled()
	{
		//arrange
		using var gpio = Substitute.For<IGpioController>();
		gpio.GetPinMode(123).Returns(PinMode.Input);
		using var pwm = Substitute.For<IPwmChannel>();
		using var pinInterface = new SystemPinInterface(123, gpio, pwm);

		//act
		var io = pinInterface.IOMode;

		//assert
		Assert.Equal(IOMode.Read, io);
		gpio.Received().OpenPin(123);
	}

	[Theory]
	[InlineData(IOMode.Read, PinMode.Input)]
	[InlineData(IOMode.Write, PinMode.Output)]
	public void CanSetIOMode(IOMode io, PinMode expected)
	{
		//arrange
		using var gpio = Substitute.For<IGpioController>();
		using var pwm = Substitute.For<IPwmChannel>();

		//act
		using var pinInterface = new SystemPinInterface(123, gpio, pwm)
		{
			Enabled = true,
			IOMode = io
		};

		//assert
		Assert.NotNull(pinInterface);
		gpio.Received().SetPinMode(123, expected);
	}

	[Fact]
	public void SettingIOModeEnablesPinIfNotEnabled()
	{
		//arrange
		using var gpio = Substitute.For<IGpioController>();
		using var pwm = Substitute.For<IPwmChannel>();

		//act
		using var pinInterface = new SystemPinInterface(123, gpio, pwm)
		{
			IOMode = IOMode.Write
		};

		//assert
		Assert.NotNull(pinInterface);
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
		using var gpio = Substitute.For<IGpioController>();
		gpio.Read(123).Returns(value);
		using var pwm = Substitute.For<IPwmChannel>();
		using var pinInterface = new SystemPinInterface(123, gpio, pwm)
		{
			Direction = Direction.Out,
			PowerMode = (IPowerMode)Activator.CreateInstance(powerModeType)!
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
		using var gpio = Substitute.For<IGpioController>();
		gpio.Read(123).Returns(PinValue.High);
		using var pwm = Substitute.For<IPwmChannel>();
		using var pinInterface = new SystemPinInterface(123, gpio, pwm)
		{
			Direction = Direction.In
		};

		//act
		var power = pinInterface.Power;

		//assert
		Assert.IsType<PowerValue>(power);
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
		using var gpio = Substitute.For<IGpioController>();
		gpio.GetPinMode(123).Returns(PinMode.Output);
		using var pwm = Substitute.For<IPwmChannel>();

		//act
		using var pinInterface = new SystemPinInterface(123, gpio, pwm)
		{
			Enabled = true,
			PowerMode = (IPowerMode)Activator.CreateInstance(powerModeType)!,
			Power = power
		};

		//assert
		Assert.NotNull(pinInterface);
		gpio.Received().Write(123, expected);
	}

	[Fact]
	public void SettingPowerSetsIOModeIfNotWrite()
	{
		//arrange
		using var gpio = Substitute.For<IGpioController>();
		gpio.GetPinMode(123).Returns(PinMode.Input);
		using var pwm = Substitute.For<IPwmChannel>();

		//act
		using var pinInterface = new SystemPinInterface(123, gpio, pwm)
		{
			Power = PowerValue.On
		};

		//assert
		Assert.NotNull(pinInterface);
		gpio.Received().SetPinMode(123, PinMode.Output);
	}

	[Fact]
	public void SettingPowerModeTurnsPinOff()
	{
		//arrange
		using var gpio = Substitute.For<IGpioController>();
		gpio.GetPinMode(123).Returns(PinMode.Input);
		using var pwm = Substitute.For<IPwmChannel>();
		using var pinInterface = new SystemPinInterface(123, gpio, pwm)
		{
			PowerMode = PowerMode.Direct
		};

		//act
		pinInterface.PowerMode = PowerMode.Differential;

		//assert
		gpio.Received().Write(123, PinValue.High);
	}

	[Fact]
	public void EnableOpensPin()
	{
		//arrange
		using var gpio = Substitute.For<IGpioController>();
		using var pwm = Substitute.For<IPwmChannel>();
		using var pinInterface = new SystemPinInterface(123, gpio, pwm)
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
		using var gpio = Substitute.For<IGpioController>();
		using var pwm = Substitute.For<IPwmChannel>();
		using var pinInterface = new SystemPinInterface(123, gpio, pwm)
		{
			Enabled = true
		};

		//act
		pinInterface.Disable();

		//assert
		Assert.False(pinInterface.Enabled);
	}

	[Theory]
	[InlineData(0, 0)]
	[InlineData(100, 1)]
	[InlineData(50, 0.5)]
	[InlineData(101, 1)]
	[InlineData(-1, 0)]
	public void StrengthSetsDutyCycle(double strength, double expected)
	{
		//arrange
		using var gpio = Substitute.For<IGpioController>();
		using var pwm = Substitute.For<IPwmChannel>();
		using var pinInterface = new SystemPinInterface(123, gpio, pwm);

		//act
		pinInterface.Strength = strength;

		//assert
		pwm.Received().DutyCycle = expected;
	}

	[Fact]
	public void SettingStrengthTurnsOffIfOnAndZeroStrength()
	{
		//arrange
		using var gpio = Substitute.For<IGpioController>();
		gpio.Read(123).Returns(PinValue.High);
		using var pwm = Substitute.For<IPwmChannel>();
		using var pinInterface = new SystemPinInterface(123, gpio, pwm);

		//act
		pinInterface.Strength = 0;

		//assert
		gpio.Received().Write(123, PinValue.Low);
	}

	[Fact]
	public void SettingStrengthDoesNotTurnOffIfOnAndNonZeroStrength()
	{
		//arrange
		using var gpio = Substitute.For<IGpioController>();
		gpio.Read(123).Returns(PinValue.High);
		using var pwm = Substitute.For<IPwmChannel>();
		using var pinInterface = new SystemPinInterface(123, gpio, pwm);

		//act
		pinInterface.Strength = 1;

		//assert
		gpio.DidNotReceive().Write(123, PinValue.Low);
	}

	[Fact]
	public void SettingStrengthDoesNotTurnOffIfAlreadyOff()
	{
		//arrange
		using var gpio = Substitute.For<IGpioController>();
		gpio.Read(123).Returns(PinValue.Low);
		using var pwm = Substitute.For<IPwmChannel>();
		using var pinInterface = new SystemPinInterface(123, gpio, pwm);

		//act
		pinInterface.Strength = 0;

		//assert
		gpio.DidNotReceive().Write(123, PinValue.Low);
	}

	[Fact]
	public void SettingStrengthTurnsOnIfOffAndNonZeroStrength()
	{
		//arrange
		using var gpio = Substitute.For<IGpioController>();
		gpio.Read(123).Returns(PinValue.Low);
		using var pwm = Substitute.For<IPwmChannel>();
		using var pinInterface = new SystemPinInterface(123, gpio, pwm);

		//act
		pinInterface.Strength = 1;

		//assert
		gpio.Received().Write(123, PinValue.High);
	}

	[Fact]
	public void SettingStrengthDoesNotTurnOnIfOffAndZeroStrength()
	{
		//arrange
		using var gpio = Substitute.For<IGpioController>();
		gpio.Read(123).Returns(PinValue.High);
		using var pwm = Substitute.For<IPwmChannel>();
		using var pinInterface = new SystemPinInterface(123, gpio, pwm);

		//act
		pinInterface.Strength = 0;

		//assert
		gpio.DidNotReceive().Write(123, PinValue.High);
	}

	[Fact]
	public void SettingStrengthDoesNotTurnOnIfAlreadyOn()
	{
		//arrange
		using var gpio = Substitute.For<IGpioController>();
		gpio.Read(123).Returns(PinValue.High);
		using var pwm = Substitute.For<IPwmChannel>();
		using var pinInterface = new SystemPinInterface(123, gpio, pwm);

		//act
		pinInterface.Strength = 1;

		//assert
		gpio.DidNotReceive().Write(123, PinValue.High);
	}

	[Fact]
	public void TurnOnSetsPowerOn()
	{
		//arrange
		using var gpio = Substitute.For<IGpioController>();
		gpio.GetPinMode(123).Returns(PinMode.Output);
		using var pwm = Substitute.For<IPwmChannel>();
		using var pinInterface = new SystemPinInterface(123, gpio, pwm)
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
		using var gpio = Substitute.For<IGpioController>();
		gpio.GetPinMode(123).Returns(PinMode.Output);
		using var pwm = Substitute.For<IPwmChannel>();
		using var pinInterface = new SystemPinInterface(123, gpio, pwm)
		{
			Power = PowerValue.On
		};

		//act
		pinInterface.TurnOff();

		//assert
		gpio.Received().Write(123, PinValue.Low);
	}

	[Fact]
	public void TurnOnDoesNotStartPWMByDefault()
	{
		//arrange
		using var gpio = Substitute.For<IGpioController>();
		gpio.GetPinMode(123).Returns(PinMode.Output);
		using var pwm = Substitute.For<IPwmChannel>();
		using var pinInterface = new SystemPinInterface(123, gpio, pwm)
		{
			Power = PowerValue.Off
		};

		//act
		pinInterface.TurnOn();

		//assert
		pwm.DidNotReceive().Start();
	}

	[Fact]
	public void TurnOnStartsPWMWhenStrengthSet()
	{
		//arrange
		using var gpio = Substitute.For<IGpioController>();
		gpio.GetPinMode(123).Returns(PinMode.Output);
		gpio.Read(123).Returns(PinValue.High);
		using var pwm = Substitute.For<IPwmChannel>();
		using var pinInterface = new SystemPinInterface(123, gpio, pwm)
		{
			Strength = 50
		};

		//act
		pinInterface.TurnOn();

		//assert
		pwm.Received().Start();
	}

	[Fact]
	public void TurnOnStopsPWMWhenFullStrength()
	{
		//arrange
		using var gpio = Substitute.For<IGpioController>();
		gpio.GetPinMode(123).Returns(PinMode.Output);
		gpio.Read(123).Returns(PinValue.High);
		using var pwm = Substitute.For<IPwmChannel>();
		using var pinInterface = new SystemPinInterface(123, gpio, pwm)
		{
			Strength = 100
		};

		//act
		pinInterface.TurnOn();

		//assert
		pwm.DidNotReceive().Start();
		pwm.Received().Stop();
	}

	[Fact]
	public void TurnOnStopsPWMWhenZeroStrength()
	{
		//arrange
		using var gpio = Substitute.For<IGpioController>();
		gpio.GetPinMode(123).Returns(PinMode.Output);
		gpio.Read(123).Returns(PinValue.High);
		using var pwm = Substitute.For<IPwmChannel>();
		using var pinInterface = new SystemPinInterface(123, gpio, pwm)
		{
			Strength = 0
		};

		//act
		pinInterface.TurnOn();

		//assert
		pwm.DidNotReceive().Start();
		pwm.Received().Stop();
	}

	[Fact]
	public void TurnOffStopsPWM()
	{
		//arrange
		using var gpio = Substitute.For<IGpioController>();
		gpio.GetPinMode(123).Returns(PinMode.Output);
		using var pwm = Substitute.For<IPwmChannel>();
		using var pinInterface = new SystemPinInterface(123, gpio, pwm)
		{
			Power = PowerValue.On
		};

		//act
		pinInterface.TurnOff();

		//assert
		pwm.Received().Stop();
	}

	[Fact]
	public void SpikeTurnsOnThenOff()
	{
		//arrange
		using var gpio = Substitute.For<IGpioController>();
		gpio.GetPinMode(123).Returns(PinMode.Output);
		using var pwm = Substitute.For<IPwmChannel>();
		using var pinInterface = new SystemPinInterface(123, gpio, pwm);

		//act
		pinInterface.Spike();

		//assert
		var calls = gpio.ReceivedCalls().Where(c => c.GetMethodInfo().Name == "Write").ToArray();
		Assert.Equal(PinValue.High, calls[0].GetArguments()[1]);
		Assert.Equal(PinValue.Low, calls[1].GetArguments()[1]);
	}

	[Fact]
	public async Task TurnOnForTurnsOffAfter()
	{
		//arrange
		using var gpio = Substitute.For<IGpioController>();
		gpio.GetPinMode(123).Returns(PinMode.Output);
		using var pwm = Substitute.For<IPwmChannel>();
		using var pinInterface = new SystemPinInterface(123, gpio, pwm);

		//act
		await pinInterface.TurnOnFor(TimeSpan.Zero);

		//assert
		var calls = gpio.ReceivedCalls().Where(c => c.GetMethodInfo().Name == "Write").ToArray();
		Assert.Equal(PinValue.High, calls[0].GetArguments()[1]);
		Assert.Equal(PinValue.Low, calls[1].GetArguments()[1]);
	}

	[Fact]
	public async Task TurnOffForTurnsOnAfter()
	{
		//arrange
		using var gpio = Substitute.For<IGpioController>();
		gpio.GetPinMode(123).Returns(PinMode.Output);
		using var pwm = Substitute.For<IPwmChannel>();
		using var pinInterface = new SystemPinInterface(123, gpio, pwm);

		//act
		await pinInterface.TurnOffFor(TimeSpan.Zero);

		//assert
		var calls = gpio.ReceivedCalls().Where(c => c.GetMethodInfo().Name == "Write").ToArray();
		Assert.Equal(PinValue.Low, calls[0].GetArguments()[1]);
		Assert.Equal(PinValue.High, calls[1].GetArguments()[1]);
	}

	[Fact]
	public void ToggleTurnsOnIfOff()
	{
		//arrange
		using var gpio = Substitute.For<IGpioController>();
		gpio.GetPinMode(123).Returns(PinMode.Output);
		gpio.Read(123).Returns(PinValue.Low);
		using var pwm = Substitute.For<IPwmChannel>();
		using var pinInterface = new SystemPinInterface(123, gpio, pwm)
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
		using var gpio = Substitute.For<IGpioController>();
		gpio.GetPinMode(123).Returns(PinMode.Output);
		gpio.Read(123).Returns(PinValue.High);
		using var pwm = Substitute.For<IPwmChannel>();
		using var pinInterface = new SystemPinInterface(123, gpio, pwm)
		{
			Power = PowerValue.On
		};

		//act
		pinInterface.Toggle();

		//assert
		gpio.Received().Write(123, PinValue.Low);
	}

	[Fact]
	public async Task CanToggleForADuration()
	{
		//arrange
		using var gpio = Substitute.For<IGpioController>();
		gpio.GetPinMode(123).Returns(PinMode.Output);
		gpio.Read(123).Returns(PinValue.Low);
		using var pwm = Substitute.For<IPwmChannel>();
		using var pinInterface = new SystemPinInterface(123, gpio, pwm);

		//act
		await pinInterface.Toggle(1000, TimeSpan.FromMilliseconds(1));

		//assert
		gpio.Received(2).Write(123, Arg.Any<PinValue>());
	}

	[Fact]
	public async Task CanToggleForSetIterations()
	{
		//arrange
		using var gpio = Substitute.For<IGpioController>();
		gpio.GetPinMode(123).Returns(PinMode.Output);
		gpio.Read(123).Returns(PinValue.Low);
		using var pwm = Substitute.For<IPwmChannel>();
		using var pinInterface = new SystemPinInterface(123, gpio, pwm);

		//act
		await pinInterface.Toggle(TimeSpan.TicksPerMillisecond, 10);

		//assert
		gpio.Received(20).Write(123, Arg.Any<PinValue>());
	}

	[Fact]
	public async Task FadeInEndsAtFullStrength()
	{
		//arrange
		using var gpio = Substitute.For<IGpioController>();
		using var pwm = Substitute.For<IPwmChannel>();
		using var pinInterface = new SystemPinInterface(123, gpio, pwm);

		//act
		await pinInterface.FadeIn(TimeSpan.Zero);

		//assert
		Assert.Equal(100, pinInterface.Strength);
	}

	[Fact]
	public async Task FadeOutEndsAtZeroStrength()
	{
		//arrange
		using var gpio = Substitute.For<IGpioController>();
		using var pwm = Substitute.For<IPwmChannel>();
		using var pinInterface = new SystemPinInterface(123, gpio, pwm);

		//act
		await pinInterface.FadeOut(TimeSpan.Zero);

		//assert
		Assert.Equal(0, pinInterface.Strength);
	}

	[Fact]
	public void FadeToScalesStrength()
	{
		//arrange
		using var gpio = Substitute.For<IGpioController>();
		using var pwm = Substitute.For<IPwmChannel>();
		using var pinInterface = new SystemPinInterface(123, gpio, pwm)
		{
			Strength = 75
		};

		//act
		_ = pinInterface.FadeTo(25, TimeSpan.FromSeconds(1));
		SpinWait.SpinUntil(() => pinInterface.Strength < 75);

		//assert
		Assert.True(pinInterface.Strength > 25);
	}

	[Fact]
	public async Task PulseSetsInitialStrength()
	{
		//arrange
		using var gpio = Substitute.For<IGpioController>();
		using var pwm = Substitute.For<IPwmChannel>();
		using var pinInterface = new SystemPinInterface(123, gpio, pwm);

		//act
		await pinInterface.Pulse(50, TimeSpan.Zero);

		//assert
		pwm.Received().DutyCycle = 0.5;
		Assert.Equal(0, pinInterface.Strength);
	}

	[Fact]
	public async Task DefaultPulseSetsToFullStrength()
	{
		//arrange
		using var gpio = Substitute.For<IGpioController>();
		using var pwm = Substitute.For<IPwmChannel>();
		using var pinInterface = new SystemPinInterface(123, gpio, pwm);

		//act
		await pinInterface.Pulse(TimeSpan.Zero);

		//assert
		pwm.Received().DutyCycle = 1;
		Assert.Equal(0, pinInterface.Strength);
	}

	[Fact]
	public void OnPowerOnEnablesIfNotEnabled()
	{
		//arrange
		using var gpio = Substitute.For<IGpioController>();
		gpio.GetPinMode(123).Returns(PinMode.Input);
		using var pwm = Substitute.For<IPwmChannel>();
		using var pinInterface = new SystemPinInterface(123, gpio, pwm);

		//act
		pinInterface.OnPowerOn(() => { });

		//assert
		gpio.Received().OpenPin(123);
	}

	[Fact]
	public void OnPowerOnPerformsAction()
	{
		//arrange
		using var gpio = Substitute.For<IGpioController>();
		gpio.GetPinMode(123).Returns(PinMode.Input);
		gpio.When(g =>
				g.RegisterCallbackForPinValueChangedEvent(123, PinEventTypes.Rising, Arg.Any<PinChangeEventHandler>()))
			.Do(c => c.Arg<PinChangeEventHandler>().Invoke(null!, null!));
		using var pwm = Substitute.For<IPwmChannel>();
		using var pinInterface = new SystemPinInterface(123, gpio, pwm)
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
		using var gpio = Substitute.For<IGpioController>();
		gpio.GetPinMode(123).Returns(PinMode.Input);
		using var pwm = Substitute.For<IPwmChannel>();
		using var pinInterface = new SystemPinInterface(123, gpio, pwm);

		//act
		pinInterface.OnPowerOff(() => { });

		//assert
		gpio.Received().OpenPin(123);
	}

	[Fact]
	public void OnPowerOffPerformsAction()
	{
		//arrange
		using var gpio = Substitute.For<IGpioController>();
		gpio.GetPinMode(123).Returns(PinMode.Input);
		gpio.When(g =>
				g.RegisterCallbackForPinValueChangedEvent(123, PinEventTypes.Falling, Arg.Any<PinChangeEventHandler>()))
			.Do(c => c.Arg<PinChangeEventHandler>().Invoke(null!, null!));

		using var pwm = Substitute.For<IPwmChannel>();
		using var pinInterface = new SystemPinInterface(123, gpio, pwm)
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
		using var gpio = Substitute.For<IGpioController>();
		gpio.GetPinMode(123).Returns(PinMode.Input);
		using var pwm = Substitute.For<IPwmChannel>();
		using var pinInterface = new SystemPinInterface(123, gpio, pwm);

		//act
		pinInterface.OnPowerChange(() => { });

		//assert
		gpio.Received().OpenPin(123);
	}

	[Fact]
	public void OnPowerChangePerformsAction()
	{
		//arrange
		using var gpio = Substitute.For<IGpioController>();
		gpio.GetPinMode(123).Returns(PinMode.Input);
		gpio.When(g =>
				g.RegisterCallbackForPinValueChangedEvent(123, Arg.Any<PinEventTypes>(),
					Arg.Any<PinChangeEventHandler>()))
			.Do(c => c.Arg<PinChangeEventHandler>().Invoke(null!, null!));
		using var pwm = Substitute.For<IPwmChannel>();
		using var pinInterface = new SystemPinInterface(123, gpio, pwm);
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
		using var gpio = Substitute.For<IGpioController>();
		gpio.GetPinMode(123).Returns(PinMode.Output);
		using var pwm = Substitute.For<IPwmChannel>();
		using var pinInterface = new SystemPinInterface(123, gpio, pwm);

		//act
		pinInterface.Dispose();

		//assert
		pwm.Received().Dispose();
		gpio.Received().ClosePin(123);
	}
}