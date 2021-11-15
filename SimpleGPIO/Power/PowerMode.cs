namespace SimpleGPIO.Power;

public static class PowerMode
{
	private static IPowerMode _differential;
	public static IPowerMode Differential => _differential ?? (_differential = new Differential());

	private static IPowerMode _direct;
	public static IPowerMode Direct => _direct ?? (_direct = new Direct());
}
