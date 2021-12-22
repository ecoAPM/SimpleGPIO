namespace SimpleGPIO.Power;

public sealed class Direct : IPowerMode
{
	public Voltage On => Voltage.High;
	public Voltage Off => Voltage.Low;
}