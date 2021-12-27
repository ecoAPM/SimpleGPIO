namespace SimpleGPIO.Power;

/// <summary>The power mode of a pin</summary>
public interface IPowerMode
{
	/// <summary>The voltage when power is on</summary>
	Voltage On { get; }

	/// <summary>The voltage when power is off</summary>
	Voltage Off { get; }
}