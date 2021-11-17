using SimpleGPIO.GPIO;

namespace SimpleGPIO.Components;

public class RGBLED
{
	private readonly IPinInterface _red;
	private readonly IPinInterface _green;
	private readonly IPinInterface _blue;

	public RGBLED(IPinInterface redPin, IPinInterface greenPin, IPinInterface bluePin)
	{
		_red = redPin;
		_green = greenPin;
		_blue = bluePin;
	}

	public void TurnRed()
	{
		_red.TurnOn();
		_green.TurnOff();
		_blue.TurnOff();
	}

	public void TurnYellow()
	{
		_red.TurnOn();
		_green.TurnOn();
		_blue.TurnOff();
	}

	public void TurnGreen()
	{
		_red.TurnOff();
		_green.TurnOn();
		_blue.TurnOff();
	}

	public void TurnCyan()
	{
		_red.TurnOff();
		_green.TurnOn();
		_blue.TurnOn();
	}

	public void TurnBlue()
	{
		_red.TurnOff();
		_green.TurnOff();
		_blue.TurnOn();
	}

	public void TurnPurple()
	{
		_red.TurnOn();
		_green.TurnOff();
		_blue.TurnOn();
	}

	public void TurnWhite()
	{
		_red.TurnOn();
		_green.TurnOn();
		_blue.TurnOn();
	}

	public void TurnOff()
	{
		_red.TurnOff();
		_green.TurnOff();
		_blue.TurnOff();
	}
}