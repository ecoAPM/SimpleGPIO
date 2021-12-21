using SimpleGPIO.Boards;
using SimpleGPIO.Components;

using var pi = new RaspberryPi();
var dial = new RotaryEncoder(pi.Pin11, pi.Pin13);

var x = 0;
dial.OnIncrease(() => Console.WriteLine(++x));
dial.OnDecrease(() => Console.WriteLine(--x));

Console.WriteLine("Press any key to exit...");
Console.ReadKey();