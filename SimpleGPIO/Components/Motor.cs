using System.ComponentModel;
using SimpleGPIO.GPIO;

namespace SimpleGPIO.Components;

/// <summary>A DC motor controlled by an L293D</summary>
public sealed class Motor
{
	/// <summary>The list of possible rotation directions</summary>
	public enum Rotation
	{
		Clockwise,
		Counterclockwise
	}

	private readonly IPinInterface? _enabled;
	private readonly IPinInterface _clockwise;
	private readonly IPinInterface? _counterclockwise;

	/// <summary>The motor's direction of rotation</summary>
	public Rotation Direction { get; set; } = Rotation.Clockwise;

	/// <summary>Creates a new DC motor</summary>
	/// <param name="enabledPin">The pin controlling if the motor is enabled</param>
	/// <param name="clockwisePin">The pin controlling clockwise rotation</param>
	/// <param name="counterclockwisePin">The pin controlling counter-clockwise rotation</param>
	public Motor(IPinInterface? enabledPin, IPinInterface clockwisePin, IPinInterface? counterclockwisePin = null)
	{
		_enabled = enabledPin;
		_clockwise = clockwisePin;
		_counterclockwise = counterclockwisePin;
	}

	/// <summary>Starts the motor running in the set direction</summary>
	public void Start()
	{
		switch (Direction)
		{
			case Rotation.Clockwise:
				_clockwise.TurnOn();
				_counterclockwise?.TurnOff();
				break;
			case Rotation.Counterclockwise:
				_clockwise.TurnOff();
				_counterclockwise?.TurnOn();
				break;
			default:
				throw new InvalidEnumArgumentException(nameof(Direction));
		}
		_enabled?.TurnOn();
	}

	/// <summary>Starts the motor running clockwise</summary>
	public void TurnClockwise()
	{
		Direction = Rotation.Clockwise;
		Start();
	}

	/// <summary>Starts the motor running counter-clockwise</summary>
	public void TurnCounterclockwise()
	{
		Direction = Rotation.Counterclockwise;
		Start();
	}

	/// <summary>Runs the motor in the set direction for a given duration</summary>
	/// <param name="length">The duration to run the motor for</param>
	/// <param name="coast">Coast once the duration is up, otherwise stop immediately</param>
	public async Task RunFor(TimeSpan length, bool coast = false)
	{
		Start();
		await Task.Delay(length);

		if (coast)
			Coast();
		else
			Stop();
	}

	/// <summary>Runs the motor clockwise for a given duration</summary>
	/// <param name="length">The duration to run the motor for</param>
	/// <param name="coast">Coast once the duration is up, otherwise stop immediately</param>
	public async Task TurnClockwiseFor(TimeSpan length, bool coast = false)
	{
		Direction = Rotation.Clockwise;
		await RunFor(length, coast);
	}

	/// <summary>Runs the motor counter-clockwise for a given duration</summary>
	/// <param name="length">The duration to run the motor for</param>
	/// <param name="coast">Coast once the duration is up, otherwise stop immediately</param>
	public async Task TurnCounterclockwiseFor(TimeSpan length, bool coast = false)
	{
		Direction = Rotation.Counterclockwise;
		await RunFor(length, coast);
	}

	/// <summary>Stops the motor immediately</summary>
	public void Stop()
	{
		_clockwise.TurnOff();
		_counterclockwise?.TurnOff();
		_enabled?.TurnOff();
	}

	/// <summary>Lets the motor coast to a stop</summary>
	public void Coast() => _enabled?.TurnOff();
}