using System.ComponentModel;

namespace SimpleGPIO.IO;

public static class IOHelpers
{
	/// <summary>Converts a direction to an I/O mode</summary>
	/// <param name="direction">The direction of the pin</param>
	/// <returns>The I/O mode of the pin</returns>
	public static IOMode ToIOMode(this Direction direction)
		=> direction switch
		{
			Direction.In => IOMode.Read,
			Direction.Out => IOMode.Write,
			_ => throw new InvalidEnumArgumentException(nameof(direction))
		};

	/// <summary>Converts an I/O mode to a direction</summary>
	/// <param name="io">The I/O mode of the pin</param>
	/// <returns>The direction of the pin</returns>
	public static Direction ToDirection(this IOMode io)
		=> io switch
		{
			IOMode.Read => Direction.In,
			IOMode.Write => Direction.Out,
			_ => throw new InvalidEnumArgumentException(nameof(io))
		};
}