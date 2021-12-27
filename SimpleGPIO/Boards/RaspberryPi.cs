using SimpleGPIO.Device;
using SimpleGPIO.GPIO;

namespace SimpleGPIO.Boards;

/// <summary>A Raspberry Pi 2/3/4</summary>
public sealed class RaspberryPi : BroadcomBoard
{
	/// <summary>Creates a new Raspberry Pi 2/3/4</summary>
	public RaspberryPi() : this(PinInterfaceFactory.NewPinInterface)
	{
	}

	/// <summary>Internal constructor for unit testing</summary>
	/// <param name="pinInterfaceFactory">The factory method to create pin interfaces</param>
	public RaspberryPi(Func<byte, IPinInterface> pinInterfaceFactory) : base(pinInterfaceFactory)
	{
	}

	/// <summary>The pin 2 rows down on the left, corresponding to GPIO2</summary>
	public IPinInterface Pin3 => GPIO2;

	/// <summary>The pin 3 rows down on the left, corresponding to GPIO3</summary>
	public IPinInterface Pin5 => GPIO3;

	/// <summary>The pin 4 rows down on the left, corresponding to GPIO4</summary>
	public IPinInterface Pin7 => GPIO4;

	/// <summary>The pin 4 rows down on the right, corresponding to GPIO14</summary>
	public IPinInterface Pin8 => GPIO14;

	/// <summary>The pin 5 rows down on the right, corresponding to GPIO15</summary>
	public IPinInterface Pin10 => GPIO15;

	/// <summary>The pin 6 rows down on the left, corresponding to GPIO17</summary>
	public IPinInterface Pin11 => GPIO17;

	/// <summary>The pin 6 rows down on the right, corresponding to GPIO18</summary>
	public IPinInterface Pin12 => GPIO18;

	/// <summary>The pin 7 rows down on the left, corresponding to GPIO27</summary>
	public IPinInterface Pin13 => GPIO27;

	/// <summary>The pin 8 rows down on the left, corresponding to GPIO22</summary>
	public IPinInterface Pin15 => GPIO22;

	/// <summary>The pin 8 rows down on the right, corresponding to GPIO23</summary>
	public IPinInterface Pin16 => GPIO23;

	/// <summary>The pin 9 rows down on the right, corresponding to GPIO24</summary>
	public IPinInterface Pin18 => GPIO24;

	/// <summary>The pin 10 rows down on the left, corresponding to GPIO10</summary>
	public IPinInterface Pin19 => GPIO10;

	/// <summary>The pin 11 rows down (10 rows up from the bottom) on the left, corresponding to GPIO9</summary>
	public IPinInterface Pin21 => GPIO9;

	/// <summary>The pin 11 rows down (10 rows up from the bottom) on the right, corresponding to GPIO25</summary>
	public IPinInterface Pin22 => GPIO25;

	/// <summary>The pin 12 rows down (9 rows up from the bottom) on the left, corresponding to GPIO11</summary>
	public IPinInterface Pin23 => GPIO11;

	/// <summary>The pin 12 rows down (9 rows up from the bottom) on the right, corresponding to GPIO8</summary>
	public IPinInterface Pin24 => GPIO8;

	/// <summary>The pin 13 rows down (8 rows up from the bottom) on the right, corresponding to GPIO7</summary>
	public IPinInterface Pin26 => GPIO7;

	/// <summary>The pin 14 rows down (7 rows up from the bottom) on the left, corresponding to GPIO0</summary>
	public IPinInterface Pin27 => GPIO0;

	/// <summary>The pin 14 rows down (7 rows up from the bottom) on the right, corresponding to GPIO1</summary>
	public IPinInterface Pin28 => GPIO1;

	/// <summary>The pin 15 rows down (6 rows up from the bottom) on the left, corresponding to GPIO5</summary>
	public IPinInterface Pin29 => GPIO5;

	/// <summary>The pin 16 rows down (5 rows up from the bottom) on the left, corresponding to GPIO6</summary>
	public IPinInterface Pin31 => GPIO6;

	/// <summary>The pin 16 rows down (5 rows up from the bottom) on the right, corresponding to GPIO12</summary>
	public IPinInterface Pin32 => GPIO12;

	/// <summary>The pin 17 rows down (4 rows up from the bottom) on the left, corresponding to GPIO13</summary>
	public IPinInterface Pin33 => GPIO13;

	/// <summary>The pin 18 rows down (3 rows up from the bottom) on the left, corresponding to GPIO19</summary>
	public IPinInterface Pin35 => GPIO19;

	/// <summary>The pin 18 rows down (3 rows up from the bottom) on the right, corresponding to GPIO16</summary>
	public IPinInterface Pin36 => GPIO16;

	/// <summary>The pin 19 rows down (2nd bottom row) on the left, corresponding to GPIO26</summary>
	public IPinInterface Pin37 => GPIO26;

	/// <summary>The pin 19 rows down (2nd bottom row) on the right, corresponding to GPIO20</summary>
	public IPinInterface Pin38 => GPIO20;

	/// <summary>The pin 20 rows down (bottom row) on the right, corresponding to GPIO21</summary>
	public IPinInterface Pin40 => GPIO21;
}