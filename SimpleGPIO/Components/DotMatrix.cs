using SimpleGPIO.GPIO;
using SimpleGPIO.Power;

namespace SimpleGPIO.Components;

public sealed class DotMatrix
{
	public IPinInterface[] Row { get; }
	public IPinInterface[] Column { get; }

	public DotMatrix(PinSet input)
	{
		Row = new[] { input.Pin9, input.Pin14, input.Pin8, input.Pin12, input.Pin1, input.Pin7, input.Pin2, input.Pin5 };
		Column = new[] { input.Pin13, input.Pin3, input.Pin4, input.Pin10, input.Pin6, input.Pin11, input.Pin15, input.Pin16 };
		foreach (var column in Column)
			column.PowerMode = PowerMode.Differential;
	}

	public void SetRows(PowerValue row1, PowerValue row2, PowerValue row3, PowerValue row4, PowerValue row5, PowerValue row6, PowerValue row7, PowerValue row8)
	{
		Row[0].Power = row1;
		Row[1].Power = row2;
		Row[2].Power = row3;
		Row[3].Power = row4;
		Row[4].Power = row5;
		Row[5].Power = row6;
		Row[6].Power = row7;
		Row[7].Power = row8;
	}

	public void SetColumns(PowerValue col1, PowerValue col2, PowerValue col3, PowerValue col4, PowerValue col5, PowerValue col6, PowerValue col7, PowerValue col8)
	{
		Column[0].Power = col1;
		Column[1].Power = col2;
		Column[2].Power = col3;
		Column[3].Power = col4;
		Column[4].Power = col5;
		Column[5].Power = col6;
		Column[6].Power = col7;
		Column[7].Power = col8;
	}

	public void SetAllRows(PowerValue power)
		=> SetRows(power, power, power, power, power, power, power, power);

	public void SetAllColumns(PowerValue power)
		=> SetColumns(power, power, power, power, power, power, power, power);

	public void SetAll(PowerValue power)
	{
		SetAllRows(power);
		SetAllColumns(power);
	}

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
}