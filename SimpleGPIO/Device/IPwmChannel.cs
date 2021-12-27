namespace SimpleGPIO.Device;

/// <summary>A wrapper around .NET's pulse width modulation implementation</summary>
public interface IPwmChannel : IDisposable
{
	/// <summary>Starts the PWM thread</summary>
	void Start();

	/// <summary>Stops the PWM thread</summary>
	void Stop();

	/// <summary>The frequency (in hertz) of the PWM</summary>
	int Frequency { get; set; }

	/// <summary>The duty cycle factor (from 0 to 1) that power is on</summary>
	double DutyCycle { get; set; }
}