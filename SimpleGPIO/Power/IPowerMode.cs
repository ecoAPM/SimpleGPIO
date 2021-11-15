namespace SimpleGPIO.Power;

public interface IPowerMode
{
	Voltage On { get; }
	Voltage Off { get; }
}
