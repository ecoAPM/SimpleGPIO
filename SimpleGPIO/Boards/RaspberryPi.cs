using System;
using SimpleGPIO.GPIO;

namespace SimpleGPIO.Boards;

public class RaspberryPi : BroadcomBoard
{
	public RaspberryPi(Func<byte, IPinInterface> newPin = null) : base(newPin)
	{
	}

	public IPinInterface Pin3 => GPIO2;
	public IPinInterface Pin5 => GPIO3;
	public IPinInterface Pin7 => GPIO4;
	public IPinInterface Pin8 => GPIO14;
	public IPinInterface Pin10 => GPIO15;
	public IPinInterface Pin11 => GPIO17;
	public IPinInterface Pin12 => GPIO18;
	public IPinInterface Pin13 => GPIO27;
	public IPinInterface Pin15 => GPIO22;
	public IPinInterface Pin16 => GPIO23;
	public IPinInterface Pin18 => GPIO24;
	public IPinInterface Pin19 => GPIO10;
	public IPinInterface Pin21 => GPIO9;
	public IPinInterface Pin22 => GPIO25;
	public IPinInterface Pin23 => GPIO11;
	public IPinInterface Pin24 => GPIO8;
	public IPinInterface Pin26 => GPIO7;
	public IPinInterface Pin27 => GPIO0;
	public IPinInterface Pin28 => GPIO1;
	public IPinInterface Pin29 => GPIO5;
	public IPinInterface Pin31 => GPIO6;
	public IPinInterface Pin32 => GPIO12;
	public IPinInterface Pin33 => GPIO13;
	public IPinInterface Pin35 => GPIO19;
	public IPinInterface Pin36 => GPIO16;
	public IPinInterface Pin37 => GPIO26;
	public IPinInterface Pin38 => GPIO20;
	public IPinInterface Pin40 => GPIO21;
}
