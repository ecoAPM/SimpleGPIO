using SimpleGPIO.Boards;
using SimpleGPIO.Components;

using var pi = new RaspberryPi();
var enabledPin = pi.Pin11;
var clockwisePin = pi.Pin13;
var counterclockwisePin = pi.Pin15;
var motor = new Motor(enabledPin, clockwisePin, counterclockwisePin);

await motor.TurnClockwiseFor(TimeSpan.FromSeconds(2));
//give it a second to fully stop before reversing
await Task.Delay(TimeSpan.FromSeconds(1));

await motor.TurnCounterclockwiseFor(TimeSpan.FromSeconds(1), true);
//give it some cooldown time before disposing,
//as counterclockwisePin turning off will abruptly stop the motor
await Task.Delay(TimeSpan.FromSeconds(2));