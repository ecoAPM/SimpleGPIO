using SimpleGPIO.Device;
using SimpleGPIO.GPIO;

namespace SimpleGPIO.Boards;

public class BroadcomBoard : IDisposable
{
	private readonly Func<byte, IPinInterface> _pinInterfaceFactory;

	public BroadcomBoard(Func<byte, IPinInterface>? pinInterfaceFactory = null)
	{
		_pinInterfaceFactory = pinInterfaceFactory ?? PinInterfaceFactory.NewPinInterface;
	}

	private readonly IPinInterface?[] _gpio = new IPinInterface[28];

	private IPinInterface GetGPIO(byte bcmIdentifier)
		=> _gpio[bcmIdentifier] ??= _pinInterfaceFactory(bcmIdentifier);

	public IPinInterface GPIO0 => GetGPIO(0);
	public IPinInterface GPIO1 => GetGPIO(1);
	public IPinInterface GPIO2 => GetGPIO(2);
	public IPinInterface GPIO3 => GetGPIO(3);
	public IPinInterface GPIO4 => GetGPIO(4);
	public IPinInterface GPIO5 => GetGPIO(5);
	public IPinInterface GPIO6 => GetGPIO(6);
	public IPinInterface GPIO7 => GetGPIO(7);
	public IPinInterface GPIO8 => GetGPIO(8);
	public IPinInterface GPIO9 => GetGPIO(9);
	public IPinInterface GPIO10 => GetGPIO(10);
	public IPinInterface GPIO11 => GetGPIO(11);
	public IPinInterface GPIO12 => GetGPIO(12);
	public IPinInterface GPIO13 => GetGPIO(13);
	public IPinInterface GPIO14 => GetGPIO(14);
	public IPinInterface GPIO15 => GetGPIO(15);
	public IPinInterface GPIO16 => GetGPIO(16);
	public IPinInterface GPIO17 => GetGPIO(17);
	public IPinInterface GPIO18 => GetGPIO(18);
	public IPinInterface GPIO19 => GetGPIO(19);
	public IPinInterface GPIO20 => GetGPIO(20);
	public IPinInterface GPIO21 => GetGPIO(21);
	public IPinInterface GPIO22 => GetGPIO(22);
	public IPinInterface GPIO23 => GetGPIO(23);
	public IPinInterface GPIO24 => GetGPIO(24);
	public IPinInterface GPIO25 => GetGPIO(25);
	public IPinInterface GPIO26 => GetGPIO(26);
	public IPinInterface GPIO27 => GetGPIO(27);

	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	protected virtual void Dispose(bool disposing)
	{
		if (!disposing)
		{
			return;
		}

		for (var id = 0; id < 28; id++)
		{
			_gpio[id]?.Dispose();
		}
	}
}