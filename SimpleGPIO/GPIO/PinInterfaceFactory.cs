using SimpleGPIO.Device;

namespace SimpleGPIO.GPIO;

internal static class PinInterfaceFactory
{
	public static IPinInterface NewPinInterface(byte pin)
		=> new SystemPinInterface(pin, GPIOController, PWM(pin));

	private static readonly IGpioController GPIOController = new GpioControllerWrapper();
	private static IPwmChannel PWM(byte pin) => new PwmChannelWrapper(pin, GPIOController);
}