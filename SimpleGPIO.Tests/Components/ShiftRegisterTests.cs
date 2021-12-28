using NSubstitute;
using SimpleGPIO.Components;
using SimpleGPIO.GPIO;
using SimpleGPIO.Power;
using SimpleGPIO.Tests.GPIO;
using Xunit;

namespace SimpleGPIO.Tests.Components;

public sealed class ShiftRegisterTests
{
	[Fact]
	public void SetPowerValuesSetsCorrectDataBits()
	{
		//arrange
		var enabled = new PinStub(1);
		var data = Substitute.For<IPinInterface>();
		var shift = new PinStub(2);
		var output = new PinStub(3);
		var clear = new PinStub(4);
		var register = new ShiftRegister(enabled, data, shift, output, clear);

		var values = new ShiftRegister.PowerSet
		{
			A = PowerValue.Off,
			B = PowerValue.On,
			C = PowerValue.Off,
			D = PowerValue.Off,
			E = PowerValue.On,
			F = PowerValue.Off,
			G = PowerValue.Off,
			H = PowerValue.On
		};

		//act
		register.SetPowerValues(values);

		//assert
		var calls = data.ReceivedCalls().ToArray();
		Assert.Equal(PowerValue.On, calls[7].GetArguments()[0]);
		Assert.Equal(PowerValue.Off, calls[6].GetArguments()[0]);
		Assert.Equal(PowerValue.Off, calls[5].GetArguments()[0]);
		Assert.Equal(PowerValue.On, calls[4].GetArguments()[0]);
		Assert.Equal(PowerValue.Off, calls[3].GetArguments()[0]);
		Assert.Equal(PowerValue.Off, calls[2].GetArguments()[0]);
		Assert.Equal(PowerValue.On, calls[1].GetArguments()[0]);
		Assert.Equal(PowerValue.Off, calls[0].GetArguments()[0]);
	}

	[Fact]
	public void SetValueSetsCorrectDataBits()
	{
		//arrange
		var enabled = new PinStub(1);
		var data = Substitute.For<IPinInterface>();
		var shift = new PinStub(2);
		var output = new PinStub(3);
		var clear = new PinStub(4);
		var register = new ShiftRegister(enabled, data, shift, output, clear);

		//act
		register.SetValue(146);

		//assert
		var calls = data.ReceivedCalls().ToArray();
		Assert.Equal(PowerValue.On, calls[0].GetArguments()[0]);
		Assert.Equal(PowerValue.Off, calls[1].GetArguments()[0]);
		Assert.Equal(PowerValue.Off, calls[2].GetArguments()[0]);
		Assert.Equal(PowerValue.On, calls[3].GetArguments()[0]);
		Assert.Equal(PowerValue.Off, calls[4].GetArguments()[0]);
		Assert.Equal(PowerValue.Off, calls[5].GetArguments()[0]);
		Assert.Equal(PowerValue.On, calls[6].GetArguments()[0]);
		Assert.Equal(PowerValue.Off, calls[7].GetArguments()[0]);
	}

	[Fact]
	public void ClearSpikesOutputWhileClearIsOn()
	{
		//arrange
		var enabled = new PinStub(1);
		var data = new PinStub(2);
		var shift = new PinStub(3);
		var output = Substitute.For<IPinInterface>();
		var clear = Substitute.For<IPinInterface>();
		var register = new ShiftRegister(enabled, data, shift, output, clear);

		//act
		register.Clear();

		//assert
		clear.Received().TurnOn();
		output.Received().Spike();
		clear.Received().TurnOff();
	}
}