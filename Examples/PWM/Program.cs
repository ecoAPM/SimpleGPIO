using SimpleGPIO.Boards;

var pi = new RaspberryPi();
var led = pi.GPIO17;
var delay = TimeSpan.FromSeconds(1);

await led.FadeIn(delay);

led.Strength = 25;
await Task.Delay(delay);

await led.FadeTo(75, delay);

led.Strength = 50;
await Task.Delay(delay);

await led.FadeOut(delay);
pi.Dispose();