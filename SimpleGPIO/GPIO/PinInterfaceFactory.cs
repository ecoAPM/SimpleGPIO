using SimpleGPIO.Device;

namespace SimpleGPIO.GPIO;

public static class PinInterfaceFactory
{
	public static IPinInterface NewPinInterface(byte pin)
		=> new SystemPinInterface(pin, GPIOController);

	private static readonly IGpioController GPIOController = new GpioControllerWrapper();
}