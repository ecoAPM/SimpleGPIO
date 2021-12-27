namespace SimpleGPIO.IO;

/// <summary>The I/O mode of a pin</summary>
public enum IOMode
{
	/// <summary>The pin is reading / receiving electricity</summary>
	Read = Direction.In,

	/// <summary>The pin is writing / outputting electricity</summary>
	Write = Direction.Out
}