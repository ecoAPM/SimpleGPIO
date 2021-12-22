using System.ComponentModel;

namespace SimpleGPIO.IO;

public static class IOHelpers
{
	public static IOMode ToIOMode(this Direction direction)
		=> direction switch
		{
			Direction.In => IOMode.Read,
			Direction.Out => IOMode.Write,
			_ => throw new InvalidEnumArgumentException(nameof(direction))
		};

	public static Direction ToDirection(this IOMode io)
		=> io switch
		{
			IOMode.Read => Direction.In,
			IOMode.Write => Direction.Out,
			_ => throw new InvalidEnumArgumentException(nameof(io))
		};

	public static Direction ToDirection(this string direction)
		=> direction.ToLower() switch
		{
			"in" => Direction.In,
			"out" => Direction.Out,
			_ => throw new InvalidEnumArgumentException(nameof(direction))
		};
}