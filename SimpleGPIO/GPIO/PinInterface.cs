using System.Diagnostics;
using SimpleGPIO.IO;
using SimpleGPIO.Power;

namespace SimpleGPIO.GPIO;

public abstract class PinInterface : IPinInterface
{
	public abstract bool Enabled { get; set; }

	public IOMode IOMode
	{
		get => Direction.ToIOMode();
		set => Direction = value.ToDirection();
	}

	public abstract Direction Direction { get; set; }

	private IPowerMode _powerMode = SimpleGPIO.Power.PowerMode.Direct;
	public IPowerMode PowerMode
	{
		get => _powerMode;
		set
		{
			_powerMode = value;
			TurnOff();
		}
	}

	public PowerValue Power
	{
		get => Voltage.ToPowerValue(PowerMode);
		set => Voltage = value == PowerValue.On ? PowerMode.On : PowerMode.Off;
	}

	public abstract Voltage Voltage { get; set; }
	public abstract double Strength { get; set; }

	public void Enable() => Enabled = true;
	public void Disable() => Enabled = false;

	public void TurnOn() => Power = PowerValue.On;
	public void TurnOff() => Power = PowerValue.Off;

	public void Spike()
	{
		TurnOn();
		TurnOff();
	}

	public async Task TurnOnFor(TimeSpan length)
	{
		TurnOn();
		await Task.Delay(length);
		TurnOff();
	}

	public async Task TurnOffFor(TimeSpan length)
	{
		TurnOff();
		await Task.Delay(length);
		TurnOn();
	}

	public void Toggle() => Power = Power == PowerValue.Off ? PowerValue.On : PowerValue.Off;

	public async Task Toggle(double hz, TimeSpan duration)
	{
		var delay = Delay(hz);
		var stopwatch = Stopwatch.StartNew();
		var expected = hz * duration.TotalSeconds;
		var count = 0;
		while (stopwatch.Elapsed.Ticks <= duration.Ticks && count++ < expected)
			await RunToggleIteration(stopwatch, delay);
	}

	public async Task Toggle(double hz, ulong iterations)
	{
		var delay = Delay(hz);
		var stopwatch = Stopwatch.StartNew();
		ulong run = 0;
		while (run++ < iterations)
			await RunToggleIteration(stopwatch, delay);
	}

	private async Task RunToggleIteration(Stopwatch stopwatch, long delay)
	{
		await RunToggleHalfIteration(stopwatch, delay);
		await RunToggleHalfIteration(stopwatch, delay);
	}

	private async Task RunToggleHalfIteration(Stopwatch stopwatch, long delay)
	{
		var start = stopwatch.Elapsed.Ticks;
		Toggle();
		var end = stopwatch.Elapsed.Ticks;
		var spent = end - start;
		await Task.Delay(TimeSpan.FromTicks(spent < delay ? delay - spent : 1));
	}

	private static long Delay(double hz) => (long)(TimeSpan.TicksPerSecond / hz / 2);

	public abstract void OnPowerOn(Action action);
	public abstract void OnPowerOff(Action action);
	public abstract void OnPowerChange(Action action);

	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	protected virtual void Dispose(bool disposing)
	{
		if (disposing)
		{
			Disable();
		}
	}
}