using SimpleGPIO.GPIO;
using SimpleGPIO.Power;

namespace SimpleGPIO.Components;

/// <summary>A seven segment display</summary>
public sealed class SevenSegmentDisplay
{
	/// <summary>The set of pins connected to the component</summary>
	public sealed class PinSet : Set<IPinInterface>
	{
	}

	/// <summary>The set of power values for each segment in the component</summary>
	public sealed class PowerSet : Set<PowerValue>
	{
	}

	public abstract class Set<T>
	{
		public T? Center { get; init; }
		public T? UpperLeft { get; init; }
		public T? Top { get; init; }
		public T? UpperRight { get; init; }
		public T? LowerLeft { get; init; }
		public T? Bottom { get; init; }
		public T? LowerRight { get; init; }
		public T? Decimal { get; init; }
	}

	/// <summary>The set of pins connected to each segment in the component</summary>
	public PinSet Segments { get; }

	/// <summary>Creates a seven segment display</summary>
	/// <param name="segments">The set of pins connected to the component</param>
	public SevenSegmentDisplay(PinSet segments)
		=> Segments = segments;

	private void SetPowerValues(PowerSet segments)
	{
		SetPowerValue(Segments.Center, segments.Center);
		SetPowerValue(Segments.UpperLeft, segments.UpperLeft);
		SetPowerValue(Segments.Top, segments.Top);
		SetPowerValue(Segments.UpperRight, segments.UpperRight);
		SetPowerValue(Segments.LowerLeft, segments.LowerLeft);
		SetPowerValue(Segments.Bottom, segments.Bottom);
		SetPowerValue(Segments.LowerRight, segments.LowerRight);
		SetPowerValue(Segments.Decimal, segments.Decimal);
	}

	private static void SetPowerValue(IPinInterface? segment, PowerValue value)
	{
		if (segment != null)
		{
			segment.Power = value;
		}
	}

	/// <summary>Displays a given character on the display</summary>
	/// <param name="character">The character to show</param>
	public void Show(char character)
	{
		var showChar = CharacterMappings.ContainsKey(character)
			? CharacterMappings[character]
			: CharacterMappings[' '];
		showChar();
	}

	private IDictionary<char, Action>? _mappings;

	private IDictionary<char, Action> CharacterMappings
		=> _mappings ??= new Dictionary<char, Action>
		{
			{ ' ', () => SetPowerValues(new PowerSet { Center = PowerValue.Off, UpperLeft = PowerValue.Off, Top = PowerValue.Off, UpperRight = PowerValue.Off, LowerLeft = PowerValue.Off, Bottom = PowerValue.Off, LowerRight = PowerValue.Off, Decimal = PowerValue.Off }) },
			{ '"', () => SetPowerValues(new PowerSet { Center = PowerValue.Off, UpperLeft = PowerValue.On, Top = PowerValue.Off, UpperRight = PowerValue.On, LowerLeft = PowerValue.Off, Bottom = PowerValue.Off, LowerRight = PowerValue.Off, Decimal = PowerValue.Off }) },
			{ '\'', () => SetPowerValues(new PowerSet { Center = PowerValue.Off, UpperLeft = PowerValue.Off, Top = PowerValue.Off, UpperRight = PowerValue.On, LowerLeft = PowerValue.Off, Bottom = PowerValue.Off, LowerRight = PowerValue.Off, Decimal = PowerValue.Off }) },
			{ '(', () => SetPowerValues(new PowerSet { Center = PowerValue.Off, UpperLeft = PowerValue.On, Top = PowerValue.On, UpperRight = PowerValue.Off, LowerLeft = PowerValue.On, Bottom = PowerValue.On, LowerRight = PowerValue.Off, Decimal = PowerValue.Off }) },
			{ ')', () => SetPowerValues(new PowerSet { Center = PowerValue.Off, UpperLeft = PowerValue.Off, Top = PowerValue.On, UpperRight = PowerValue.On, LowerLeft = PowerValue.Off, Bottom = PowerValue.On, LowerRight = PowerValue.On, Decimal = PowerValue.Off }) },
			{ ',', () => SetPowerValues(new PowerSet { Center = PowerValue.Off, UpperLeft = PowerValue.Off, Top = PowerValue.Off, UpperRight = PowerValue.Off, LowerLeft = PowerValue.Off, Bottom = PowerValue.Off, LowerRight = PowerValue.On, Decimal = PowerValue.Off }) },
			{ '-', () => SetPowerValues(new PowerSet { Center = PowerValue.On, UpperLeft = PowerValue.Off, Top = PowerValue.Off, UpperRight = PowerValue.Off, LowerLeft = PowerValue.Off, Bottom = PowerValue.Off, LowerRight = PowerValue.Off, Decimal = PowerValue.Off }) },
			{ '.', () => SetPowerValues(new PowerSet { Center = PowerValue.Off, UpperLeft = PowerValue.Off, Top = PowerValue.Off, UpperRight = PowerValue.Off, LowerLeft = PowerValue.Off, Bottom = PowerValue.Off, LowerRight = PowerValue.Off, Decimal = PowerValue.On }) },
			{ '/', () => SetPowerValues(new PowerSet { Center = PowerValue.On, UpperLeft = PowerValue.Off, Top = PowerValue.Off, UpperRight = PowerValue.On, LowerLeft = PowerValue.On, Bottom = PowerValue.Off, LowerRight = PowerValue.Off, Decimal = PowerValue.Off }) },

			{ '0', () => SetPowerValues(new PowerSet { Center = PowerValue.Off, UpperLeft = PowerValue.On, Top = PowerValue.On, UpperRight = PowerValue.On, LowerLeft = PowerValue.On, Bottom = PowerValue.On, LowerRight = PowerValue.On, Decimal = PowerValue.Off }) },
			{ '1', () => SetPowerValues(new PowerSet { Center = PowerValue.Off, UpperLeft = PowerValue.Off, Top = PowerValue.Off, UpperRight = PowerValue.On, LowerLeft = PowerValue.Off, Bottom = PowerValue.Off, LowerRight = PowerValue.On, Decimal = PowerValue.Off }) },
			{ '2', () => SetPowerValues(new PowerSet { Center = PowerValue.On, UpperLeft = PowerValue.Off, Top = PowerValue.On, UpperRight = PowerValue.On, LowerLeft = PowerValue.On, Bottom = PowerValue.On, LowerRight = PowerValue.Off, Decimal = PowerValue.Off }) },
			{ '3', () => SetPowerValues(new PowerSet { Center = PowerValue.On, UpperLeft = PowerValue.Off, Top = PowerValue.On, UpperRight = PowerValue.On, LowerLeft = PowerValue.Off, Bottom = PowerValue.On, LowerRight = PowerValue.On, Decimal = PowerValue.Off }) },
			{ '4', () => SetPowerValues(new PowerSet { Center = PowerValue.On, UpperLeft = PowerValue.On, Top = PowerValue.Off, UpperRight = PowerValue.On, LowerLeft = PowerValue.Off, Bottom = PowerValue.Off, LowerRight = PowerValue.On, Decimal = PowerValue.Off }) },
			{ '5', () => SetPowerValues(new PowerSet { Center = PowerValue.On, UpperLeft = PowerValue.On, Top = PowerValue.On, UpperRight = PowerValue.Off, LowerLeft = PowerValue.Off, Bottom = PowerValue.On, LowerRight = PowerValue.On, Decimal = PowerValue.Off }) },
			{ '6', () => SetPowerValues(new PowerSet { Center = PowerValue.On, UpperLeft = PowerValue.On, Top = PowerValue.On, UpperRight = PowerValue.Off, LowerLeft = PowerValue.On, Bottom = PowerValue.On, LowerRight = PowerValue.On, Decimal = PowerValue.Off }) },
			{ '7', () => SetPowerValues(new PowerSet { Center = PowerValue.Off, UpperLeft = PowerValue.Off, Top = PowerValue.On, UpperRight = PowerValue.On, LowerLeft = PowerValue.Off, Bottom = PowerValue.Off, LowerRight = PowerValue.On, Decimal = PowerValue.Off }) },
			{ '8', () => SetPowerValues(new PowerSet { Center = PowerValue.On, UpperLeft = PowerValue.On, Top = PowerValue.On, UpperRight = PowerValue.On, LowerLeft = PowerValue.On, Bottom = PowerValue.On, LowerRight = PowerValue.On, Decimal = PowerValue.Off }) },
			{ '9', () => SetPowerValues(new PowerSet { Center = PowerValue.On, UpperLeft = PowerValue.On, Top = PowerValue.On, UpperRight = PowerValue.On, LowerLeft = PowerValue.Off, Bottom = PowerValue.On, LowerRight = PowerValue.On, Decimal = PowerValue.Off }) },

			{ '<', () => _mappings?['c']() },
			{ '=', () => SetPowerValues(new PowerSet { Center = PowerValue.On, UpperLeft = PowerValue.Off, Top = PowerValue.Off, UpperRight = PowerValue.Off, LowerLeft = PowerValue.Off, Bottom = PowerValue.On, LowerRight = PowerValue.Off, Decimal = PowerValue.Off }) },
			{ '>', () => SetPowerValues(new PowerSet { Center = PowerValue.On, UpperLeft = PowerValue.Off, Top = PowerValue.Off, UpperRight = PowerValue.Off, LowerLeft = PowerValue.Off, Bottom = PowerValue.On, LowerRight = PowerValue.On, Decimal = PowerValue.Off }) },

			{ 'A', () => SetPowerValues(new PowerSet { Center = PowerValue.On, UpperLeft = PowerValue.On, Top = PowerValue.On, UpperRight = PowerValue.On, LowerLeft = PowerValue.On, Bottom = PowerValue.Off, LowerRight = PowerValue.On, Decimal = PowerValue.Off }) },
			{ 'B', () => _mappings?['8']() },
			{ 'C', () => SetPowerValues(new PowerSet { Center = PowerValue.Off, UpperLeft = PowerValue.On, Top = PowerValue.On, UpperRight = PowerValue.Off, LowerLeft = PowerValue.On, Bottom = PowerValue.On, LowerRight = PowerValue.Off, Decimal = PowerValue.Off }) },
			{ 'D', () => _mappings?['0']() },
			{ 'E', () => SetPowerValues(new PowerSet { Center = PowerValue.On, UpperLeft = PowerValue.On, Top = PowerValue.On, UpperRight = PowerValue.Off, LowerLeft = PowerValue.On, Bottom = PowerValue.On, LowerRight = PowerValue.Off, Decimal = PowerValue.Off }) },
			{ 'F', () => SetPowerValues(new PowerSet { Center = PowerValue.On, UpperLeft = PowerValue.On, Top = PowerValue.On, UpperRight = PowerValue.Off, LowerLeft = PowerValue.On, Bottom = PowerValue.Off, LowerRight = PowerValue.Off, Decimal = PowerValue.Off }) },
			{ 'G', () => SetPowerValues(new PowerSet { Center = PowerValue.Off, UpperLeft = PowerValue.On, Top = PowerValue.On, UpperRight = PowerValue.Off, LowerLeft = PowerValue.On, Bottom = PowerValue.On, LowerRight = PowerValue.On, Decimal = PowerValue.Off }) },
			{ 'H', () => SetPowerValues(new PowerSet { Center = PowerValue.On, UpperLeft = PowerValue.On, Top = PowerValue.Off, UpperRight = PowerValue.On, LowerLeft = PowerValue.On, Bottom = PowerValue.Off, LowerRight = PowerValue.On, Decimal = PowerValue.Off }) },
			{ 'I', () => SetPowerValues(new PowerSet { Center = PowerValue.Off, UpperLeft = PowerValue.On, Top = PowerValue.Off, UpperRight = PowerValue.Off, LowerLeft = PowerValue.On, Bottom = PowerValue.Off, LowerRight = PowerValue.Off, Decimal = PowerValue.Off }) },
			{ 'J', () => SetPowerValues(new PowerSet { Center = PowerValue.Off, UpperLeft = PowerValue.Off, Top = PowerValue.Off, UpperRight = PowerValue.On, LowerLeft = PowerValue.On, Bottom = PowerValue.On, LowerRight = PowerValue.On, Decimal = PowerValue.Off }) },
			{ 'K', () => _mappings?['H']() },
			{ 'L', () => SetPowerValues(new PowerSet { Center = PowerValue.Off, UpperLeft = PowerValue.On, Top = PowerValue.Off, UpperRight = PowerValue.Off, LowerLeft = PowerValue.On, Bottom = PowerValue.On, LowerRight = PowerValue.Off, Decimal = PowerValue.Off }) },
			{ 'M', () => SetPowerValues(new PowerSet { Center = PowerValue.Off, UpperLeft = PowerValue.On, Top = PowerValue.On, UpperRight = PowerValue.On, LowerLeft = PowerValue.On, Bottom = PowerValue.Off, LowerRight = PowerValue.On, Decimal = PowerValue.Off }) },
			{ 'N', () => _mappings?['H']() },
			{ 'O', () => _mappings?['0']() },
			{ 'P', () => SetPowerValues(new PowerSet { Center = PowerValue.On, UpperLeft = PowerValue.On, Top = PowerValue.On, UpperRight = PowerValue.On, LowerLeft = PowerValue.On, Bottom = PowerValue.Off, LowerRight = PowerValue.Off, Decimal = PowerValue.Off }) },
			{ 'Q', () => SetPowerValues(new PowerSet { Center = PowerValue.Off, UpperLeft = PowerValue.On, Top = PowerValue.On, UpperRight = PowerValue.On, LowerLeft = PowerValue.On, Bottom = PowerValue.On, LowerRight = PowerValue.On, Decimal = PowerValue.On }) },
			{ 'R', () => _mappings?['A']() },
			{ 'S', () => _mappings?['5']() },
			{ 'T', () => SetPowerValues(new PowerSet { Center = PowerValue.Off, UpperLeft = PowerValue.On, Top = PowerValue.On, UpperRight = PowerValue.Off, LowerLeft = PowerValue.On, Bottom = PowerValue.Off, LowerRight = PowerValue.Off, Decimal = PowerValue.Off }) },
			{ 'U', () => SetPowerValues(new PowerSet { Center = PowerValue.Off, UpperLeft = PowerValue.On, Top = PowerValue.Off, UpperRight = PowerValue.On, LowerLeft = PowerValue.On, Bottom = PowerValue.On, LowerRight = PowerValue.On, Decimal = PowerValue.Off }) },
			{ 'V', () => SetPowerValues(new PowerSet { Center = PowerValue.On, UpperLeft = PowerValue.On, Top = PowerValue.Off, UpperRight = PowerValue.On, LowerLeft = PowerValue.On, Bottom = PowerValue.Off, LowerRight = PowerValue.Off, Decimal = PowerValue.Off }) },
			{ 'W', () => _mappings?['U']() },
			{ 'X', () => _mappings?['H']() },
			{ 'Y', () => _mappings?['4']() },
			{ 'Z', () => _mappings?['2']() },

			{ '[', () => _mappings?['(']() },
			{ '\\', () => SetPowerValues(new PowerSet { Center = PowerValue.On, UpperLeft = PowerValue.On, Top = PowerValue.Off, UpperRight = PowerValue.Off, LowerLeft = PowerValue.Off, Bottom = PowerValue.Off, LowerRight = PowerValue.On, Decimal = PowerValue.Off }) },
			{ ']', () => _mappings?[')']() },
			{ '^', () => SetPowerValues(new PowerSet { Center = PowerValue.Off, UpperLeft = PowerValue.On, Top = PowerValue.On, UpperRight = PowerValue.On, LowerLeft = PowerValue.Off, Bottom = PowerValue.Off, LowerRight = PowerValue.Off, Decimal = PowerValue.Off }) },
			{ '_', () => SetPowerValues(new PowerSet { Center = PowerValue.Off, UpperLeft = PowerValue.Off, Top = PowerValue.Off, UpperRight = PowerValue.Off, LowerLeft = PowerValue.Off, Bottom = PowerValue.On, LowerRight = PowerValue.Off, Decimal = PowerValue.Off }) },
			{ '`', () => SetPowerValues(new PowerSet { Center = PowerValue.Off, UpperLeft = PowerValue.On, Top = PowerValue.Off, UpperRight = PowerValue.Off, LowerLeft = PowerValue.Off, Bottom = PowerValue.Off, LowerRight = PowerValue.Off, Decimal = PowerValue.Off }) },

			{ 'a', () => SetPowerValues(new PowerSet { Center = PowerValue.On, UpperLeft = PowerValue.Off, Top = PowerValue.On, UpperRight = PowerValue.On, LowerLeft = PowerValue.On, Bottom = PowerValue.On, LowerRight = PowerValue.On, Decimal = PowerValue.Off }) },
			{ 'b', () => SetPowerValues(new PowerSet { Center = PowerValue.On, UpperLeft = PowerValue.On, Top = PowerValue.Off, UpperRight = PowerValue.Off, LowerLeft = PowerValue.On, Bottom = PowerValue.On, LowerRight = PowerValue.On, Decimal = PowerValue.Off }) },
			{ 'c', () => SetPowerValues(new PowerSet { Center = PowerValue.On, UpperLeft = PowerValue.Off, Top = PowerValue.Off, UpperRight = PowerValue.Off, LowerLeft = PowerValue.On, Bottom = PowerValue.On, LowerRight = PowerValue.Off, Decimal = PowerValue.Off }) },
			{ 'd', () => SetPowerValues(new PowerSet { Center = PowerValue.On, UpperLeft = PowerValue.Off, Top = PowerValue.Off, UpperRight = PowerValue.On, LowerLeft = PowerValue.On, Bottom = PowerValue.On, LowerRight = PowerValue.On, Decimal = PowerValue.Off }) },
			{ 'e', () => SetPowerValues(new PowerSet { Center = PowerValue.On, UpperLeft = PowerValue.On, Top = PowerValue.On, UpperRight = PowerValue.On, LowerLeft = PowerValue.On, Bottom = PowerValue.On, LowerRight = PowerValue.Off, Decimal = PowerValue.Off }) },
			{ 'f', () => _mappings?['F']() },
			{ 'g', () => _mappings?['9']() },
			{ 'h', () => SetPowerValues(new PowerSet { Center = PowerValue.On, UpperLeft = PowerValue.On, Top = PowerValue.Off, UpperRight = PowerValue.Off, LowerLeft = PowerValue.On, Bottom = PowerValue.Off, LowerRight = PowerValue.On, Decimal = PowerValue.Off }) },
			{ 'i', () => SetPowerValues(new PowerSet { Center = PowerValue.Off, UpperLeft = PowerValue.Off, Top = PowerValue.Off, UpperRight = PowerValue.Off, LowerLeft = PowerValue.On, Bottom = PowerValue.Off, LowerRight = PowerValue.Off, Decimal = PowerValue.Off }) },
			{ 'j', () => SetPowerValues(new PowerSet { Center = PowerValue.Off, UpperLeft = PowerValue.Off, Top = PowerValue.Off, UpperRight = PowerValue.Off, LowerLeft = PowerValue.Off, Bottom = PowerValue.On, LowerRight = PowerValue.On, Decimal = PowerValue.Off }) },
			{ 'k', () => _mappings?['h']() },
			{ 'l', () => _mappings?['I']() },
			{ 'm', () => SetPowerValues(new PowerSet { Center = PowerValue.On, UpperLeft = PowerValue.Off, Top = PowerValue.Off, UpperRight = PowerValue.Off, LowerLeft = PowerValue.On, Bottom = PowerValue.Off, LowerRight = PowerValue.On, Decimal = PowerValue.Off }) },
			{ 'n', () => _mappings?['m']() },
			{ 'o', () => SetPowerValues(new PowerSet { Center = PowerValue.On, UpperLeft = PowerValue.Off, Top = PowerValue.Off, UpperRight = PowerValue.Off, LowerLeft = PowerValue.On, Bottom = PowerValue.On, LowerRight = PowerValue.On, Decimal = PowerValue.Off }) },
			{ 'p', () => _mappings?['P']() },
			{ 'q', () => SetPowerValues(new PowerSet { Center = PowerValue.On, UpperLeft = PowerValue.On, Top = PowerValue.On, UpperRight = PowerValue.On, LowerLeft = PowerValue.Off, Bottom = PowerValue.Off, LowerRight = PowerValue.On, Decimal = PowerValue.Off }) },
			{ 'r', () => SetPowerValues(new PowerSet { Center = PowerValue.On, UpperLeft = PowerValue.Off, Top = PowerValue.Off, UpperRight = PowerValue.Off, LowerLeft = PowerValue.On, Bottom = PowerValue.Off, LowerRight = PowerValue.Off, Decimal = PowerValue.Off }) },
			{ 's', () => _mappings?['S']() },
			{ 't', () => SetPowerValues(new PowerSet { Center = PowerValue.On, UpperLeft = PowerValue.On, Top = PowerValue.Off, UpperRight = PowerValue.Off, LowerLeft = PowerValue.On, Bottom = PowerValue.On, LowerRight = PowerValue.Off, Decimal = PowerValue.Off }) },
			{ 'u', () => SetPowerValues(new PowerSet { Center = PowerValue.Off, UpperLeft = PowerValue.Off, Top = PowerValue.Off, UpperRight = PowerValue.Off, LowerLeft = PowerValue.On, Bottom = PowerValue.On, LowerRight = PowerValue.On, Decimal = PowerValue.Off }) },
			{ 'v', () => _mappings?['u']() },
			{ 'w', () => _mappings?['u']() },
			{ 'x', () => _mappings?['X']() },
			{ 'y', () => _mappings?['Y']() },
			{ 'z', () => _mappings?['Z']() },

			{ '{', () => _mappings?['(']() },
			{ '|', () => _mappings?['I']() },
			{ '}', () => _mappings?[')']() },
			{ '~', () => SetPowerValues(new PowerSet { Center = PowerValue.Off, UpperLeft = PowerValue.Off, Top = PowerValue.On, UpperRight = PowerValue.Off, LowerLeft = PowerValue.Off, Bottom = PowerValue.Off, LowerRight = PowerValue.Off, Decimal = PowerValue.Off }) },
		};
}