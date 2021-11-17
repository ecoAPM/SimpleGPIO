using System.Device.Gpio;
using SimpleGPIO.GPIO;

namespace SimpleGPIO.Boards;

public static class PinInterfaceFactory
{
	public static IPinInterface NewPinInterface(byte bcmIdentifier)
		=> new SystemPinInterface(bcmIdentifier, GpioController);

	private static GpioControllerWrapper GpioController => new GpioControllerWrapper(PinNumberingScheme.Logical);
}