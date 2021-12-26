using SimpleGPIO.Boards;
using SimpleGPIO.Components;

using var pi = new RaspberryPi();
var register = new ShiftRegister(pi.Pin13, pi.Pin11, pi.Pin15, pi.Pin16, pi.Pin18);

for (byte x = 0; x < byte.MaxValue; x++)
{
	Console.WriteLine(x);
	register.SetValue(x);
	await Task.Delay(200);
}

await Task.Delay(400);
register.Clear();