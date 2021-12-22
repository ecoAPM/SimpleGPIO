namespace SimpleGPIO.Power;

public sealed class Differential : IPowerMode
{
	public Voltage On => Voltage.Low;
	public Voltage Off => Voltage.High;
}