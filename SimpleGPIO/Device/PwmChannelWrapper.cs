using System.Device.Pwm;
using System.Device.Pwm.Drivers;

namespace SimpleGPIO.Device;

internal sealed class PwmChannelWrapper : IPwmChannel
{
	private readonly PwmChannel _pwm;

	public PwmChannelWrapper(byte pin, IGpioController controller)
		=> _pwm = new SoftwarePwmChannel(pin, 120, 0, false, controller.Base, false);

	public int Frequency
	{
		get => _pwm.Frequency;
		set => _pwm.Frequency = value;
	}

	public double DutyCycle
	{
		get => _pwm.DutyCycle;
		set => _pwm.DutyCycle = value;
	}

	public void Start() => _pwm.Start();
	public void Stop() => _pwm.Stop();

	public void Dispose() => _pwm.Dispose();
}