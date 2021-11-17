namespace SimpleGPIO.Power;

public class Direct : IPowerMode
{
	public Voltage On => Voltage.High;
	public Voltage Off => Voltage.Low;
}