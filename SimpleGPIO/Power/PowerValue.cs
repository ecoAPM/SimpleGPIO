namespace SimpleGPIO.Power;

/// <summary>The power value of a pin</summary>
public enum PowerValue
{
	/// <summary>Power is on, electricity flowing through the pin</summary>
	On = 1,

	/// <summary>Power is off, electricity not flowing through the pin</summary>
	Off = 0
}