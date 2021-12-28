using SimpleGPIO.GPIO;

namespace SimpleGPIO.Boards;

/// <summary>A device with a BCM2xxx series chip</summary>
public abstract class BroadcomBoard : IDisposable
{
	private readonly Func<byte, IPinInterface> _pinInterfaceFactory;

	protected BroadcomBoard(Func<byte, IPinInterface> pinInterfaceFactory)
	{
		_pinInterfaceFactory = pinInterfaceFactory;
	}

	private readonly IPinInterface?[] _gpio = new IPinInterface[28];

	private IPinInterface GetGPIO(byte bcmIdentifier)
		=> _gpio[bcmIdentifier] ??= _pinInterfaceFactory(bcmIdentifier);

	/// <summary>The pin labeled GPIO0</summary>
	public IPinInterface GPIO0 => GetGPIO(0);

	/// <summary>The pin labeled GPIO1</summary>
	public IPinInterface GPIO1 => GetGPIO(1);

	/// <summary>The pin labeled GPIO2</summary>
	public IPinInterface GPIO2 => GetGPIO(2);

	/// <summary>The pin labeled GPIO3</summary>
	public IPinInterface GPIO3 => GetGPIO(3);

	/// <summary>The pin labeled GPIO4</summary>
	public IPinInterface GPIO4 => GetGPIO(4);

	/// <summary>The pin labeled GPIO5</summary>
	public IPinInterface GPIO5 => GetGPIO(5);

	/// <summary>The pin labeled GPIO6</summary>
	public IPinInterface GPIO6 => GetGPIO(6);

	/// <summary>The pin labeled GPIO7</summary>
	public IPinInterface GPIO7 => GetGPIO(7);

	/// <summary>The pin labeled GPIO8</summary>
	public IPinInterface GPIO8 => GetGPIO(8);

	/// <summary>The pin labeled GPIO9</summary>
	public IPinInterface GPIO9 => GetGPIO(9);

	/// <summary>The pin labeled GPIO10</summary>
	public IPinInterface GPIO10 => GetGPIO(10);

	/// <summary>The pin labeled GPIO11</summary>
	public IPinInterface GPIO11 => GetGPIO(11);

	/// <summary>The pin labeled GPIO12</summary>
	public IPinInterface GPIO12 => GetGPIO(12);

	/// <summary>The pin labeled GPIO13</summary>
	public IPinInterface GPIO13 => GetGPIO(13);

	/// <summary>The pin labeled GPIO14</summary>
	public IPinInterface GPIO14 => GetGPIO(14);

	/// <summary>The pin labeled GPIO15</summary>
	public IPinInterface GPIO15 => GetGPIO(15);

	/// <summary>The pin labeled GPIO16</summary>
	public IPinInterface GPIO16 => GetGPIO(16);

	/// <summary>The pin labeled GPIO17</summary>
	public IPinInterface GPIO17 => GetGPIO(17);

	/// <summary>The pin labeled GPIO18</summary>
	public IPinInterface GPIO18 => GetGPIO(18);

	/// <summary>The pin labeled GPIO19</summary>
	public IPinInterface GPIO19 => GetGPIO(19);

	/// <summary>The pin labeled GPIO21</summary>
	public IPinInterface GPIO20 => GetGPIO(20);

	/// <summary>The pin labeled GPIO21</summary>
	public IPinInterface GPIO21 => GetGPIO(21);

	/// <summary>The pin labeled GPIO22</summary>
	public IPinInterface GPIO22 => GetGPIO(22);

	/// <summary>The pin labeled GPIO23</summary>
	public IPinInterface GPIO23 => GetGPIO(23);

	/// <summary>The pin labeled GPIO24</summary>
	public IPinInterface GPIO24 => GetGPIO(24);

	/// <summary>The pin labeled GPIO25</summary>
	public IPinInterface GPIO25 => GetGPIO(25);

	/// <summary>The pin labeled GPIO26</summary>
	public IPinInterface GPIO26 => GetGPIO(26);

	/// <summary>The pin labeled GPIO27</summary>
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