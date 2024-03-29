using SimpleGPIO.GPIO;
using SimpleGPIO.IO;
using SimpleGPIO.Power;

namespace SimpleGPIO.Tests.GPIO;

public sealed class PinStub : PinInterface
{
	public PinStub(byte pin) => Pin = pin;

	public byte Pin { get; }

	public override bool Enabled { get; set; }
	public override Direction Direction { get; set; }

	public override Voltage Voltage { get; set; }

	protected override void RefreshPWM()
	{
	}

	public override void OnPowerOn(Action action) => action();
	public override void OnPowerOff(Action action) => action();
	public override void OnPowerChange(Action action) => action();
}