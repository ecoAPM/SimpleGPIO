using SimpleGPIO.Boards;
using SimpleGPIO.Components;

using var pi = new RaspberryPi();

var segments = new SevenSegmentDisplay.PinSet
{
	Center = pi.GPIO12,
	UpperLeft = pi.GPIO23,
	Top = pi.GPIO24,
	UpperRight = pi.GPIO25,
	LowerLeft = pi.GPIO17,
	Bottom = pi.GPIO27,
	LowerRight = pi.GPIO22,
	Decimal = pi.GPIO5
};
var display = new SevenSegmentDisplay(segments);

Console.WriteLine("Enter characters, or press enter to exit");
var keyInfo = new ConsoleKeyInfo();
do
{
	display.Show(keyInfo.KeyChar);
	keyInfo = Console.ReadKey();
} while (keyInfo.Key != ConsoleKey.Enter);

Console.WriteLine();