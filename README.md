# SimpleGPIO [![Build Status](https://travis-ci.org/stevedesmond-ca/SimpleGPIO.svg?branch=master)](https://travis-ci.org/stevedesmond-ca/SimpleGPIO)
A simple, low-ceremony GPIO library for all your IoT needs

## Overview

`SimpleGPIO` takes a high-level, object-oriented approach to IoT programming, in the same way that high-level programming languages provide features that help abstract what's happening on the metal away from the code.

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

rgbLED.TurnRed();
rgbLED.TurnOrange();
rgbLED.TurnYellow();
rgbLED.TurnGreen();
rgbLED.TurnCyan();
rgbLED.TurnBlue();
rgbLED.TurnPurple();
rgbLED.TurnWhite();
rgbLED.TurnOff();
```

### 7-Segment Display

7-segment displays are currently supported for direct connections to GPIO pins (support for bit-shift registers coming soon) and can be passed a character (all ASCII letters, numbers, and several other symbols)

```C#
var display = new SevenSegmentDisplay(centerPin, upperLeftPin, topPin, upperRightPin, lowerLeftPin, bottomPin, lowerRightPin, /*optional*/decimalPin);
display.Show('A');
display.Show('B');
display.Show('C');
display.Show('1');
display.Show('2');
display.Show('3');
```

Custom characters can also be displayed with:
```C#
//same order:          center,        upper-left,     top,           upper-right,    lower-left,    bottom,         lower-right,   decimal (optional)
display.SetPowerValues(PowerValue.On, PowerValue.Off, PowerValue.On, PowerValue.Off, PowerValue.On, PowerValue.Off, PowerValue.On, PowerValue.Off);
```

## How can I help?

First, thank you for your enthusiasm! I'd love feedback on how you felt using this. If you had an awesome experience, let me know on [Twitter](https://twitter.com/intent/tweet?text=.@stevedesmond_ca&hashtags=SimpleGPIO). If you had any problems, feel free to [file an issue](https://github.com/stevedesmond-ca/SimpleGPIO/issues/new).

If you're looking to contribute code, but don't have any ideas of your own, there are some specific things I'd love help with over in the Issues tab!
