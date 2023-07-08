using System.Text;
using NSubstitute;
using SimpleGPIO.GPIO;
using SimpleGPIO.Power;
using SimpleGPIO.Tests.GPIO;
using Xunit;

namespace SimpleGPIO.Tests.SPI;

public sealed class InputTests
{
	[Fact]
	public void CanSendPowerValue()
	{
		//arrange
		var data = Substitute.For<IPinInterface>();
		var clock = new PinStub(2);
		var input = new InputStub(data, clock);

		//act
		input.Send(PowerValue.On);

		//assert
		data.Received().Power = PowerValue.On;
	}

	[Fact]
	public void SendByteSendsCorrectPowerValues()
	{
		//arrange
		var data = Substitute.For<IPinInterface>();
		var clock = new PinStub(2);
		var input = new InputStub(data, clock);

		//act
		input.Send(146);

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
	public void SendPowerArraySendsCorrectPowerValues()
	{
		//arrange
		var data = Substitute.For<IPinInterface>();
		var clock = new PinStub(2);
		var input = new InputStub(data, clock);
		var values = new[] { PowerValue.On, PowerValue.Off, PowerValue.Off, PowerValue.On };

		//act
		input.Send(values);

		//assert
		var calls = data.ReceivedCalls().ToArray();
		Assert.Equal(PowerValue.On, calls[0].GetArguments()[0]);
		Assert.Equal(PowerValue.Off, calls[1].GetArguments()[0]);
		Assert.Equal(PowerValue.Off, calls[2].GetArguments()[0]);
		Assert.Equal(PowerValue.On, calls[3].GetArguments()[0]);
	}

	[Fact]
	public void SendByteArraySendsCorrectPowerValues()
	{
		//arrange

		var data = Substitute.For<IPinInterface>();
		var clock = new PinStub(2);
		var input = new InputStub(data, clock);
		var bytesIn = Encoding.ASCII.GetBytes("Abc123");

		//act
		input.Send(bytesIn);

		//assert
		var bit = data.ReceivedCalls()
			.Select(c => c.GetArguments()[0]).Cast<PowerValue>()
			.Select(p => p == PowerValue.On ? (byte)1 : (byte)0)
			.ToArray();

		var bytesOut = new byte[6];
		for (var byteNum = 0; byteNum < bytesOut.Length; byteNum++)
		{
			byte byteSum = 0;
			for (var bitNum = 0; bitNum < 8; bitNum++)
			{
				byteSum += (byte)(bit[8 * byteNum + bitNum] << (7 - bitNum));
			}

			bytesOut[byteNum] = byteSum;
		}

		var output = Encoding.ASCII.GetString(bytesOut);
		Assert.Equal("Abc123", output);
	}
}