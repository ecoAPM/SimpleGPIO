namespace SimpleGPIO.Device;

public interface IPwmChannel : IDisposable
{
	void Start();
	void Stop();
	int Frequency { get; set; }
	double DutyCycle { get; set; }
}