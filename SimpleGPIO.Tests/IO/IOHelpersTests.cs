using SimpleGPIO.IO;
using Xunit;

namespace SimpleGPIO.Tests.IO;

public sealed class IOHelpersTests
{
	[Theory]
	[InlineData(Direction.In, IOMode.Read)]
	[InlineData(Direction.Out, IOMode.Write)]
	public void CanGetIOModeFromDirection(Direction direction, IOMode expected)
		=> Assert.Equal(expected, direction.ToIOMode());

	[Theory]
	[InlineData(IOMode.Read, Direction.In)]
	[InlineData(IOMode.Write, Direction.Out)]
	public void CanGetDirectionFromIOMode(IOMode io, Direction expected)
		=> Assert.Equal(expected, io.ToDirection());
}