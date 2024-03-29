# SimpleGPIO

A simple, low-ceremony GPIO library for all your IoT needs

[![NuGet version](https://img.shields.io/nuget/v/SimpleGPIO?logo=nuget&label=Install)](https://nuget.org/packages/SimpleGPIO)
[![CI](https://github.com/ecoAPM/SimpleGPIO/actions/workflows/CI.yml/badge.svg)](https://github.com/ecoAPM/SimpleGPIO/actions/workflows/CI.yml)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=ecoAPM_SimpleGPIO&metric=coverage)](https://sonarcloud.io/dashboard?id=ecoAPM_SimpleGPIO)

[![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=ecoAPM_SimpleGPIO&metric=sqale_rating)](https://sonarcloud.io/dashboard?id=ecoAPM_SimpleGPIO)
[![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=ecoAPM_SimpleGPIO&metric=reliability_rating)](https://sonarcloud.io/dashboard?id=ecoAPM_SimpleGPIO)
[![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=ecoAPM_SimpleGPIO&metric=security_rating)](https://sonarcloud.io/dashboard?id=ecoAPM_SimpleGPIO)

## Overview

`SimpleGPIO` takes a high-level, object-oriented approach to IoT programming, in the same way that high-level programming languages provide features that help abstract what's happening on the metal away from the code.

## Requirements

`SimpleGPIO` is a .NET Standard 2.0 library, and therefore should work with all actively supported .NET runtimes.

## Installation

Simply add the `SimpleGPIO` library to your project from NuGet.

## Initialization

Instantiate a new board to be able to access its GPIO header:
```C#
var pi = new RaspberryPi();
```

If you're using a dependency injection container, you can register the board as a singleton to be used elsewhere in the application:
```C#
services.AddSingleton<RaspberryPi>();
```

## Accessing GPIO pins

GPIO pins can be accessed by both their physical location on the board, and/or their Broadcom identifier GPIO#.
```C#
var redLED = pi.Pin16;
var sameRedLED = pi.GPIO23;
```

## Moving electrons

`SimpleGPIO` provides many ways to turn power on or off, depending on your preferences.

The simplest way is to use the built-in helper methods:
```C#
redLED.TurnOn();
redLED.TurnOff();
```

If you prefer assigning values:
```C#
redLED.Power = PowerValue.On;
redLED.Power = PowerValue.Off;
```

At the lowest level, you can directly set the voltage going out of the pin:
```C#
redLED.Voltage = Voltage.High; //on
redLED.Voltage = Voltage.Low;  //off
```

### Power Modes

All of the above examples assume the default `Direct` power mode, where the positive terminal of the LED is connected to the GPIO pin, and the negative terminal is connected to the ground pin.

If, instead, you want to supply constant power by, e.g. the 3v3 pin, and the have the GPIO pin supply (or not supply) resistance, you can use the `Differential` power mode, where `PowerValue.On == Voltage.Low` and `PowerValue.Off == Voltage.High`:
```C#
var yellowLED = pi.Pin18;
yellowLED.PowerMode = PowerMode.Differential;
yellowLED.TurnOn();
```

## Timed Power

Pins can be turned on or off for specific lengths of time via the following:
```C#
var led = pi.Pin18;
led.TurnOnFor(TimeSpan.FromSeconds(1)); //will turn off after 1 second

led.TurnOn();
led.TurnOffFor(TimeSpan.FromSeconds(0.5)); //will turn back on after 0.5 seconds
```

## Techno Dance Parties

There are some helper methods for toggling values. If power is currently on, toggling it will turn it off; if power is off, toggling will turn it on:
```C#
redLED.Toggle();
```

If you want to repeat the toggle at a given frequency, for a set amount of time, pass in the frequency and a `TimeSpan` as parameters:
```C#
redLED.Toggle(3, TimeSpan.FromSeconds(5));
```
This will flash the red LED 3 times per second, for 5 seconds.

Alternatively, you can toggle power a set number of times by passing in a number as the second parameter. The following will flash the red LED 3 times over 1.5 seconds:
```C#
redLED.Toggle(2, 3);
```

### Pulse Width Modulation

Pins can have their "strength" set using Pulse Width Modulation (PWM) via the `Strength` property on the pin:
```C#
var led = pi.Pin18;
led.Strength = 50; //valid range: 0-100
```

There are also several helper methods to make smooth transitions easier:
```C#
led.FadeIn(TimeSpan.FromSeconds(1));
led.FadeOut(TimeSpan.FromSeconds(2));
led.FadeTo(50,TimeSpan.FromSeconds(0.5));

led.Pulse(TimeSpan.FromSeconds(1));
led.Pulse(50, TimeSpan.FromSeconds(1));
```

## What about inputs?

Input components such as buttons can be declared the same way as output components, and the `Power` and `Voltage` can be read from the new variable:
```C#
var button = pi.Pin11;
var isPressed = button.Power == PowerValue.On;
```

The `Direct` Power Mode for an input component expects power from e.g. the 3v3 pin, so that electricity flows through to the GPIO pin when the button is depressed.

## Reacting to Change

Three methods are provided on a pin that accept an `Action` as a parameter, so that when that pin's state changes, some subsequent steps can be performed:
```C#
var button = pi.Pin11;
var redLED = pi.Pin16;
var buzzer = pi.Pin18;

button.OnPowerOn(() => redLED.TurnOn());
button.OnPowerOff(() => redLED.TurnOff());
redLED.OnPowerChange(() => buzzer.Toggle(1, 1));
```

Whenever the button is pressed down, the LED will turn on. When the button is released, the LED will turn off. Whenever the LED turns on or off, the buzzer will beep for half a second (reminder why: because Toggle will complete a single cycle at 1Hz, which means 0.5s on, then 0.5s off).

## Cleaning up

If you want to turn off everything that was turned on while your application was running, simply `Dispose()` of your `RaspberryPi` at the end of your code.

```C#
pi.Dispose();
```

This will turn off and close all open GPIO pins. As with all `IDisposable`s, this also works if you wrap the `RaspberryPi` you're using in a `using(){}` block.

## Components

Several components have been implemented to help make development easier.

### RGB LED

The RGB LED contains a separate pin for red, green, and blue, which can be combined to show different colors.

```C#
var redPin = pi.Pin11;
var greenPin = pi.Pin16;
var bluePin = pi.Pin18;

var rgbLED = new RGBLED(redPin, greenPin, bluePin);
```

Colors can then be set using the `SetColor()` method:
```C#
rgbLED.SetColor(Color.Red);
rgbLED.SetColor(Color.Yellow);
rgbLED.SetColor(Color.Purple);
```

Several helpers also exist:
```C#
rgbLED.FadeTo(Color.White, TimeSpan.FromSeconds(1));
rgbLED.Pulse(Color.Green, TimeSpan.FromSeconds(0.5));
rgbLED.TurnOff(); // same as rgbLED.SetColor(Color.Black);
```

See [the example](Examples/Components/RGBLED/Program.cs) for more details.

### Rotary Encoder

Rotary encoders have actions that can be performed when the dial is turned.

```C#
var dial = new RotaryEncoder(clockPin, dataPin);
dial.OnIncrease(() => Console.WriteLine("up"));
dial.OnDecrease(() => Console.WriteLine("down"));
```

Built-in button functionality is not yet supported.

See [the example](Examples/Components/RotartEncoder/Program.cs) for more details.

### Seven-Segment Display

Seven-segment displays are currently supported for direct connections to GPIO pins (support for shift register input coming soon) and can be passed a character (all ASCII letters, numbers, and several other symbols)

```C#
var segments = new PinSet
{
	Center = centerPin,
	UpperLeft = upperLeftPin,
	Top = topPin,
 	UpperRight = upperRightPin,
	LowerLeft = lowerLeftPin,
	Bottom = bottomPin,
	LowerRight = lowerRightPin,
	Decimal = decimalPin //optional
};
var display = new SevenSegmentDisplay(segments);
display.Show('A');
display.Show('B');
display.Show('C');
display.Show('1');
display.Show('2');
display.Show('3');
```

Custom characters can also be displayed passing a `PowerSet` to `SetPowerValues()`:

```C#
var custom = new PowerSet
{
	Center = PowerValue.On,
	UpperLeft = PowerValue.Off,
	Top = PowerValue.On,
	UpperRight = PowerValue.Off,
	LowerLeft = PowerValue.On,
	Bottom = PowerValue.Off,
	LowerRight = PowerValue.On,
	Decimal = PowerValue.Off //optional
};
display.SetPowerValues(custom);
```

See [the example](Examples/Components/SevenSegmentDisplay/Program.cs) for more details.

### Dot Matrix Display

The dot matrix display required 16 inputs, one for each row and one for each column. Like the seven segment display, these are initialized via a `PinSet`, with pins numbered counter-clockwise starting from the bottom left:
```C#
var set = new DotMatrix.PinSet
{
	//rows
	Pin1 = pi.GPIO5, Pin2 = pi.GPIO7, Pin3 = pi.GPIO12, Pin4 = pi.GPIO13, Pin5 = pi.GPIO8, Pin6 = pi.GPIO15, Pin7 = pi.GPIO6, Pin8 = pi.GPIO3,

	//columns
	Pin9 = pi.GPIO1, Pin10 = pi.GPIO14, Pin11 = pi.GPIO16, Pin12 = pi.GPIO4, Pin13 = pi.GPIO11, Pin14 = pi.GPIO2, Pin15 = pi.GPIO17, Pin16 = pi.GPIO18
};
var matrix = new DotMatrix(set);
```

You can then set rows or columns individually or all together:
```C#
matrix.SetAllRows(PowerValue.On);
matrix.SetAllColumns(PowerValue.Off);

matrix.SetRows(new DotMatrix.PowerSet { ... });
matrix.SetColumns(new DotMatrix.PowerSet { ... });
```

See [the example](Examples/Components/DotMatrix/Program.cs) for more details.

### Bidirectional Motor

The wiring required to safely run a motor is rather complicated. The code, however, can be quite eloquent. The `Motor` component assumes an L293D-compatible driver.

```C#
var enabledPin = pi.Pin11;
var clockwisePin = pi.Pin13; //name assumes connected to L293D pin 1A
var counterclockwisePin = pi.Pin15; // name assumes connected to L293D pin 2A
var motor = new Motor(enabledPin, clockwisePin, counterclockwisePin);
motor.Direction = Rotation.Clockwise;
motor.Start();
motor.Stop();

motor.Direction = Rotation.Counterclockwise;
motor.Start();
motor.Coast();

motor.TurnClockwise();
motor.TurnCounterclockwise();
motor.TurnClockwiseFor(TimeSpan.FromSeconds(1));
motor.TurnCounterclockwiseFor(TimeSpan.FromSeconds(2), true); //optional parameter to coast instead of stop
```

If you never need to coast (only stop) and your `enabled` pin is always on (e.g. 3.3 or 5V), you can pass `null` as the first constructor parameter.

If using all 4 inputs on a single driver, declare another `Motor` to handle inputs 3 and 4.

To drive a single-direction motor (by only having input 1 connected), simply pass `null` as the `counterclockwisePin` to the `Motor` constructor. Counterclockwise methods are not expected to function under this condition.

See [the example](Examples/Components/Motor/Program.cs) for more details.

### Shift Register

A shift register allows you to control more outputs than you have inputs. The `ShiftRegister` component abstracts the implementation details of the 595-style integrated circuit away, so you can simply send the data you want as a `byte`!

```C#
var enabledPin = pi.Pin11;
var dataPin = pi.Pin13;
var shiftPin = pi.Pin15;
var outputPin = pi.Pin16; //aka "latch"
var clearPin = pi.Pin18;
var register = new ShiftRegister(enabledPin, dataPin, shiftPin, outputPin, clearPin);

register.SetValue(255); //sets all 8 bits to On
register.SetValue(0b11111111); //does the same

//these two are also identical to each other
register.SetValue(0b10101010);
var values = new PowerSet
{
	A = PowerValue.On,
	B = PowerValue.Off,
	C = PowerValue.On,
	D = PowerValue.Off,
	E = PowerValue.On,
	F = PowerValue.Off,
	G = PowerValue.On,
	H = PowerValue.Off
};
register.SetPowerValues(values);

//you can also accomplish this more manually
dataPin.TurnOn();
shiftPin.Spike();
dataPin.TurnOff();
shiftPin.Spike();
outputPin.Spike();
```

Similar to the `Motor` component, the `enabled` and `clear` parameters are optional; if you choose to have the register always on/enabled by connecting it to the 3.3 or 5V rail, set the first param to `null`. If you never need to clear, and that pin is also connected to 3.3 or 5V, just leave the last param out.

See [the example](Examples/Components/ShiftRegister/Program.cs) for more details.

## "Wow, this is great! How can I help?"

First, thank you for your enthusiasm! I'd love feedback on how you felt using this. If you had an awesome experience, let me know on [Twitter](https://twitter.com/intent/tweet?text=.@stevedesmond_ca&hashtags=SimpleGPIO). If you had any problems, feel free to [file an issue](https://github.com/stevedesmond-ca/SimpleGPIO/issues/new).

If you're looking to contribute code, but don't have any ideas of your own, there are some specific things I'd love help with over in the Issues tab!
