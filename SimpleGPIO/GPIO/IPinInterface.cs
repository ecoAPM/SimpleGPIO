using SimpleGPIO.IO;
using SimpleGPIO.Power;

namespace SimpleGPIO.GPIO;

/// <summary>An interface to a GPIO pin</summary>
public interface IPinInterface : IDisposable
{
	/// <summary>Whether the pin is enabled or not</summary>
	bool Enabled { get; set; }

	/// <summary>The I/O mode of the pin</summary>
	IOMode IOMode { get; set; }

	/// <summary>The direction of the pin</summary>
	Direction Direction { get; set; }

	/// <summary>The power mode of the pin</summary>
	IPowerMode PowerMode { get; set; }

	/// <summary>The power value of the pin</summary>
	PowerValue Power { get; set; }

	/// <summary>The voltage of the pin</summary>
	Voltage Voltage { get; set; }

	/// <summary>The percent strength (from 0 to 100) of the pin</summary>
	double Strength { get; set; }

	/// <summary>Enables the pin for use</summary>
	void Enable();

	/// <summary>Disables the pin</summary>
	void Disable();

	/// <summary>Turns on the pin</summary>
	void TurnOn();

	/// <summary>Turns off the pin</summary>
	void TurnOff();

	/// <summary>Quickly turns the pin on then off</summary>
	void Spike();

	/// <summary>Turns the pin on for the specified duration, then turns it off</summary>
	/// <param name="length">The duration to turn on for</param>
	Task TurnOnFor(TimeSpan length);

	/// <summary>Turns the pin off for the specified duration, then turns it off</summary>
	/// <param name="length">The duration to turn off for</param>
	Task TurnOffFor(TimeSpan length);

	/// <summary>Turns the pin on if it is off, or off if it is on</summary>
	void Toggle();

	/// <summary>Toggles the pin's power for a given duration</summary>
	/// <param name="hz">The frequency to toggle power at</param>
	/// <param name="duration">The duration to toggle power for</param>
	Task Toggle(double hz, TimeSpan duration);

	/// <summary>Toggles the pin's power for a given number of iterations</summary>
	/// <param name="hz">The frequency to toggle power at</param>
	/// <param name="iterations">The number of times to toggle power</param>
	Task Toggle(double hz, ulong iterations);

	/// <summary>Fades the pin's strength from its current value up to 100%</summary>
	/// <param name="duration">The duration to stretch the fade over</param>
	Task FadeIn(TimeSpan duration);

	/// <summary>Fades the pin's strength from its current value down to 0%</summary>
	/// <param name="duration">The duration to stretch the fade over</param>
	Task FadeOut(TimeSpan duration);

	/// <summary>Fades the pin's strength from its current value to the given value</summary>
	/// <param name="strength">The strength to fade to</param>
	/// <param name="duration">The duration to stretch the fade over</param>
	Task FadeTo(double strength, TimeSpan duration);

	/// <summary>Sets the pin to full strength, then fades it out</summary>
	/// <param name="duration">The duration to stretch the fade over</param>
	Task Pulse(TimeSpan duration);

	/// <summary>Sets the pin to the given strength, then fades it out</summary>
	/// <param name="strength">The maximum strength of the pulse</param>
	/// <param name="duration">The duration to stretch the fade over</param>
	Task Pulse(double strength, TimeSpan duration);

	/// <summary>Perform an action when power to the pin is turned on</summary>
	/// <param name="action">The action to perform</param>
	void OnPowerOn(Action action);

	/// <summary>Perform an action when power to the pin is turned off</summary>
	/// <param name="action">The action to perform</param>
	void OnPowerOff(Action action);

	/// <summary>Perform an action when power to the pin is turned on or off</summary>
	/// <param name="action">The action to perform</param>
	void OnPowerChange(Action action);
}