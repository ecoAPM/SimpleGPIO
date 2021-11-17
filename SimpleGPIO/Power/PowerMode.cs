namespace SimpleGPIO.Power;

public static class PowerMode
{
	private static IPowerMode? _differential;
	public static IPowerMode Differential => _differential ??= new Differential();

	private static IPowerMode? _direct;
	public static IPowerMode Direct => _direct ??= new Direct();
}