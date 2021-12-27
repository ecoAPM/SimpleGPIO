namespace SimpleGPIO.Power;

/// <summary>Differential power mode, where electricity flows from a 3.3V or 5V pin to a GPIO pin</summary>
public sealed class Differential : IPowerMode
{
	public Voltage On => Voltage.Low;
	public Voltage Off => Voltage.High;
}