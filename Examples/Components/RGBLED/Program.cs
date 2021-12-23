using System.Drawing;
using SimpleGPIO.Boards;
using SimpleGPIO.Components;

using var pi = new RaspberryPi();
var led = new RGBLED(pi.GPIO5, pi.GPIO6, pi.GPIO13);
var wait = TimeSpan.FromSeconds(0.5);

led.SetColor(Color.Red);
await Task.Delay(wait);

led.SetColor(Color.Yellow);
await Task.Delay(wait);

led.SetColor(Color.Lime);
await Task.Delay(wait);

led.SetColor(Color.Cyan);
await Task.Delay(wait);

led.SetColor(Color.Blue);
await Task.Delay(wait);

led.SetColor(Color.Magenta);
await Task.Delay(wait);

led.SetColor(Color.Black);
await Task.Delay(wait);

led.SetColor(Color.White);
await Task.Delay(wait);