using SimpleGPIO.GPIO;

namespace SimpleGPIO.Tests.SPI;

public sealed class InputStub : SimpleGPIO.SPI.Input
{
	public InputStub(IPinInterface data, IPinInterface clock) : base(data, clock)
	{
	}
}