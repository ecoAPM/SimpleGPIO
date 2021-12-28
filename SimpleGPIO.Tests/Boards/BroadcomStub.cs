using SimpleGPIO.Boards;
using SimpleGPIO.GPIO;

namespace SimpleGPIO.Tests.Boards;

internal sealed class BroadcomStub : BroadcomBoard
{
	public BroadcomStub(Func<byte, IPinInterface> pinInterfaceFactory) : base(pinInterfaceFactory)
	{
	}
}