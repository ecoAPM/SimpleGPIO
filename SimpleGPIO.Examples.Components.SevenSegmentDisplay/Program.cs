using System;
using SimpleGPIO.Boards;

namespace SimpleGPIO.Examples.Components.SevenSegmentDisplay;

public static class Program
{
	public static void Main()
	{
		using var pi = new RaspberryPi();
		var display = new SimpleGPIO.Components.SevenSegmentDisplay(pi.GPIO23, pi.GPIO24, pi.GPIO25, pi.GPIO12, pi.GPIO17, pi.GPIO27, pi.GPIO22, pi.GPIO5);

		Console.WriteLine("Enter characters, or press enter to exit");
		var keyInfo = new ConsoleKeyInfo();
		do
		{
			display.Show(keyInfo.KeyChar);
			keyInfo = Console.ReadKey();
		} while (keyInfo.Key != ConsoleKey.Enter);

		Console.WriteLine();
	}
}
