namespace SimpleGPIO.Power;

/// <summary>The power mode of a pin</summary>
public static class PowerMode
{
	private static IPowerMode? _direct;
	private static IPowerMode? _differential;

	/// <summary>Direct power mode, where electricity flows from a GPIO pin to a ground pin</summary>
	public static IPowerMode Direct => _direct ??= new Direct();

	/// <summary>Differential power mode, where electricity flows from a 3.3V or 5V pin to a GPIO pin</summary>
	public static IPowerMode Differential => _differential ??= new Differential();

}