namespace SimpleGPIO.Power
{
    public class Differential : IPowerMode
    {
        public Voltage On => Voltage.Low;
        public Voltage Off => Voltage.High;
    }
}