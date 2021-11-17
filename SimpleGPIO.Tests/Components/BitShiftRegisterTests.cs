using NSubstitute;
using SimpleGPIO.Components;
using SimpleGPIO.GPIO;
using SimpleGPIO.Power;
using SimpleGPIO.Tests.GPIO;
using Xunit;

namespace SimpleGPIO.Tests.Components;

public class BitShiftRegisterTests
{
	[Theory]
	[InlineData(146, 7, PowerValue.On)]
	[InlineData(146, 6, PowerValue.Off)]
	[InlineData(146, 5, PowerValue.Off)]
	[InlineData(146, 4, PowerValue.On)]
	[InlineData(146, 3, PowerValue.Off)]
	[InlineData(146, 2, PowerValue.Off)]
	[InlineData(146, 1, PowerValue.On)]
	[InlineData(146, 0, PowerValue.Off)]
	public void CanSetPowerValueForBinaryDigit(byte value, byte digit, PowerValue expected)
	{
		//arrange/act
		var result = BitShiftRegister.GetBinaryDigitPowerValue(value, digit);

		//assert
		Assert.Equal(expected, result);
	}

	[Fact]
	public void SetPowerValuesSetsCorrectDataBits()
	{
		//arrange
		var enabled = new StubPinInterface(1);
		var data = Substitute.For<IPinInterface>();
		var shift = new StubPinInterface(2);
		var output = new StubPinInterface(3);
		var clear = new StubPinInterface(4);
		var register = new BitShiftRegister(enabled, data, shift, output, clear);

		//act
		register.SetPowerValues(PowerValue.Off, PowerValue.On, PowerValue.Off, PowerValue.Off, PowerValue.On, PowerValue.Off, PowerValue.Off, PowerValue.On);

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
		var enabled = new StubPinInterface(1);
		var data = Substitute.For<IPinInterface>();
		var shift = new StubPinInterface(2);
		var output = new StubPinInterface(3);
		var clear = new StubPinInterface(4);
		var register = new BitShiftRegister(enabled, data, shift, output, clear);

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
		var enabled = new StubPinInterface(1);
		var data = new StubPinInterface(2);
		var shift = new StubPinInterface(3);
		var output = Substitute.For<IPinInterface>();
		var clear = Substitute.For<IPinInterface>();
		var register = new BitShiftRegister(enabled, data, shift, output, clear);

		//act
		register.Clear();

		//assert
		clear.Received().TurnOn();
		output.Received().Spike();
		clear.Received().TurnOff();
	}
}