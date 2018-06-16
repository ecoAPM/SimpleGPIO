namespace SimpleGPIO.Power
{
    public static class PowerHelpers
    {
        public static PowerValue ToPowerValue(this Voltage voltage, IPowerMode powerMode)
            => voltage == powerMode.On
                ? PowerValue.On
                : PowerValue.Off;
    }
}
