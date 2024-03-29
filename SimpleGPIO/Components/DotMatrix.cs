using SimpleGPIO.GPIO;
using SimpleGPIO.Power;

namespace SimpleGPIO.Components;

/// <summary>An 8x8 dot matrix LED array</summary>
public sealed class DotMatrix
{
	/// <summary>The set of pins representing the rows of the matrix</summary>
	public IPinInterface[] Row { get; }

	/// <summary>The set of pins representing the rows of the matrix</summary>
	public IPinInterface[] Column { get; }

	/// <summary>Creates a new 8x8 dot matrix LED array</summary>
	/// <param name="input">The set of pins controlling the component</param>
	public DotMatrix(PinSet input)
	{
		Row = new[] { input.Pin9, input.Pin14, input.Pin8, input.Pin12, input.Pin1, input.Pin7, input.Pin2, input.Pin5 };
		Column = new[] { input.Pin13, input.Pin3, input.Pin4, input.Pin10, input.Pin6, input.Pin11, input.Pin15, input.Pin16 };

		foreach (var column in Column)
		{
			column.PowerMode = PowerMode.Differential;
		}
	}

	/// <summary>Sets the values for each row in the display</summary>
	/// <param name="rows">The power values for each row</param>
	public void SetRows(PowerSet rows)
	{
		Row[0].Power = rows.Item1;
		Row[1].Power = rows.Item2;
		Row[2].Power = rows.Item3;
		Row[3].Power = rows.Item4;
		Row[4].Power = rows.Item5;
		Row[5].Power = rows.Item6;
		Row[6].Power = rows.Item7;
		Row[7].Power = rows.Item8;
	}

	/// <summary>Sets the values for each column in the display</summary>
	/// <param name="columns">The power values for each column</param>
	public void SetColumns(PowerSet columns)
	{
		Column[0].Power = columns.Item1;
		Column[1].Power = columns.Item2;
		Column[2].Power = columns.Item3;
		Column[3].Power = columns.Item4;
		Column[4].Power = columns.Item5;
		Column[5].Power = columns.Item6;
		Column[6].Power = columns.Item7;
		Column[7].Power = columns.Item8;
	}

	/// <summary>Sets each row to the same value</summary>
	/// <param name="power">The power value to set each row to</param>
	public void SetAllRows(PowerValue power)
		=> SetRows(new PowerSet
		{
			Item1 = power,
			Item2 = power,
			Item3 = power,
			Item4 = power,
			Item5 = power,
			Item6 = power,
			Item7 = power,
			Item8 = power
		});

	/// <summary>Sets each column to the same value</summary>
	/// <param name="power">The power value to set each column to</param>
	public void SetAllColumns(PowerValue power)
		=> SetColumns(new PowerSet
		{
			Item1 = power,
			Item2 = power,
			Item3 = power,
			Item4 = power,
			Item5 = power,
			Item6 = power,
			Item7 = power,
			Item8 = power
		});

	/// <summary>Sets all pixels to the same value</summary>
	/// <param name="power">The power value to set each pixel to</param>
	public void SetAll(PowerValue power)
	{
		SetAllRows(power);
		SetAllColumns(power);
	}

	/// <summary>The set of pins input into the component</summary>
	public sealed class PinSet
	{
		public IPinInterface Pin1 { get; init; } = null!;
		public IPinInterface Pin2 { get; init; } = null!;
		public IPinInterface Pin3 { get; init; } = null!;
		public IPinInterface Pin4 { get; init; } = null!;
		public IPinInterface Pin5 { get; init; } = null!;
		public IPinInterface Pin6 { get; init; } = null!;
		public IPinInterface Pin7 { get; init; } = null!;
		public IPinInterface Pin8 { get; init; } = null!;
		public IPinInterface Pin9 { get; init; } = null!;
		public IPinInterface Pin10 { get; init; } = null!;
		public IPinInterface Pin11 { get; init; } = null!;
		public IPinInterface Pin12 { get; init; } = null!;
		public IPinInterface Pin13 { get; init; } = null!;
		public IPinInterface Pin14 { get; init; } = null!;
		public IPinInterface Pin15 { get; init; } = null!;
		public IPinInterface Pin16 { get; init; } = null!;
	}

	/// <summary>The set of power values for a row or column</summary>
	public sealed class PowerSet
	{
		public PowerValue Item1 { get; init; }
		public PowerValue Item2 { get; init; }
		public PowerValue Item3 { get; init; }
		public PowerValue Item4 { get; init; }
		public PowerValue Item5 { get; init; }
		public PowerValue Item6 { get; init; }
		public PowerValue Item7 { get; init; }
		public PowerValue Item8 { get; init; }
	}
}