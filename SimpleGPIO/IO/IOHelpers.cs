using System.ComponentModel;

namespace SimpleGPIO.IO;

public static class IOHelpers
{
	public static IOMode ToIOMode(this Direction direction)
	{
		switch (direction)
		{
			case Direction.In:
				return IOMode.Read;
			case Direction.Out:
				return IOMode.Write;
			default:
				throw new InvalidEnumArgumentException(nameof(direction));
		}
	}

	public static Direction ToDirection(this IOMode io)
	{
		switch (io)
		{
			case IOMode.Read:
				return Direction.In;
			case IOMode.Write:
				return Direction.Out;
			default:
				throw new InvalidEnumArgumentException(nameof(io));
		}
	}

	public static Direction ToDirection(this string direction)
	{
		switch (direction.ToLower())
		{
			case "in":
				return Direction.In;
			case "out":
				return Direction.Out;
			default:
				throw new InvalidEnumArgumentException(nameof(direction));
		}
	}
}