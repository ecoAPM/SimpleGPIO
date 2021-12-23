using SimpleGPIO.Boards;

var pi = new RaspberryPi();
var led = pi.GPIO17;
var delay = TimeSpan.FromSeconds(1);

led.Strength = 75;
await Task.Delay(delay);

led.Strength = 25;
await Task.Delay(delay);

led.Strength = 100;
await Task.Delay(delay);

led.Strength = 50;
await Task.Delay(delay);

led.TurnOff();
await Task.Delay(delay);

pi.Dispose();