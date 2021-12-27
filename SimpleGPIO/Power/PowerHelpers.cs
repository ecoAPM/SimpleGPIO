namespace SimpleGPIO.Power;

public static class PowerHelpers
{
	/// <summary>Converts a voltage value to a power value</summary>
	/// <param name="voltage">The voltage to convert</param>
	/// <param name="powerMode">The power mode of the pin</param>
	/// <returns>The power value of the pin</returns>
	public static PowerValue ToPowerValue(this Voltage voltage, IPowerMode powerMode)
		=> voltage == powerMode.On
			? PowerValue.On
			: PowerValue.Off;
}