using System.Drawing;
using SimpleGPIO.Boards;
using SimpleGPIO.Components;

using var pi = new RaspberryPi();
var led = new RGBLED(pi.GPIO5, pi.GPIO6, pi.GPIO13);
var delay = TimeSpan.FromSeconds(0.5);

led.SetColor(Color.Red);
await Task.Delay(delay);

led.SetColor(Color.Lime);
await Task.Delay(delay);

led.SetColor(Color.Blue);
await Task.Delay(delay);

await led.FadeTo(Color.Yellow, delay);
await led.FadeTo(Color.Magenta, delay);
await led.FadeOut(delay);