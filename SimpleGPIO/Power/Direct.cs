namespace SimpleGPIO.Power;

/// <summary>Direct power mode, where electricity flows from a GPIO pin to a ground pin</summary>
public sealed class Direct : IPowerMode
{
	public Voltage On => Voltage.High;
	public Voltage Off => Voltage.Low;
}