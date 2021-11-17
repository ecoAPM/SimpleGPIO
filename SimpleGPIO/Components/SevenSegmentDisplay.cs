using SimpleGPIO.GPIO;
using SimpleGPIO.Power;

namespace SimpleGPIO.Components;

public class SevenSegmentDisplay
{
	public IPinInterface Center { get; }
	public IPinInterface UpperLeft { get; }
	public IPinInterface Top { get; }
	public IPinInterface UpperRight { get; }
	public IPinInterface LowerLeft { get; }
	public IPinInterface Bottom { get; }
	public IPinInterface LowerRight { get; }
	public IPinInterface? Decimal { get; }

	public SevenSegmentDisplay(IPinInterface centerPin, IPinInterface upperLeftPin, IPinInterface topPin, IPinInterface upperRightPin, IPinInterface lowerLeftPin, IPinInterface bottomPin, IPinInterface lowerRightPin, IPinInterface? decimalPin = null)
	{
		Center = centerPin;
		UpperLeft = upperLeftPin;
		Top = topPin;
		UpperRight = upperRightPin;
		LowerLeft = lowerLeftPin;
		Bottom = bottomPin;
		LowerRight = lowerRightPin;
		Decimal = decimalPin;
	}

	public void SetPowerValues(PowerValue center, PowerValue upperLeft, PowerValue top, PowerValue upperRight, PowerValue lowerLeft, PowerValue bottom, PowerValue lowerRight, PowerValue decimalPoint = PowerValue.Off)
	{
		Center.Power = center;
		UpperLeft.Power = upperLeft;
		Top.Power = top;
		UpperRight.Power = upperRight;
		LowerLeft.Power = lowerLeft;
		Bottom.Power = bottom;
		LowerRight.Power = lowerRight;

		if (Decimal != null)
			Decimal.Power = decimalPoint;
	}

	public void Show(char character)
	{
		var showChar = CharacterMappings.ContainsKey(character)
			? CharacterMappings[character]
			: CharacterMappings[' '];
		showChar();
	}

	private IDictionary<char, Action>? _mappings;
	private IDictionary<char, Action> CharacterMappings
	{
		get
		{
			return _mappings ??= new Dictionary<char, Action>
				{
					{ ' ', () => SetPowerValues(PowerValue.Off, PowerValue.Off, PowerValue.Off, PowerValue.Off, PowerValue.Off, PowerValue.Off, PowerValue.Off) },
					{ '"', () => SetPowerValues(PowerValue.Off, PowerValue.On , PowerValue.Off, PowerValue.On , PowerValue.Off, PowerValue.Off, PowerValue.Off) },
					{ '\'',() => SetPowerValues(PowerValue.Off, PowerValue.Off, PowerValue.Off, PowerValue.On , PowerValue.Off, PowerValue.Off, PowerValue.Off) },
					{ '(', () => SetPowerValues(PowerValue.Off, PowerValue.On , PowerValue.On , PowerValue.Off, PowerValue.On , PowerValue.On , PowerValue.Off) },
					{ ')', () => SetPowerValues(PowerValue.Off, PowerValue.Off, PowerValue.On , PowerValue.On , PowerValue.Off, PowerValue.On , PowerValue.On ) },
					{ ',', () => SetPowerValues(PowerValue.Off, PowerValue.Off, PowerValue.Off, PowerValue.Off, PowerValue.Off, PowerValue.Off, PowerValue.On ) },
					{ '-', () => SetPowerValues(PowerValue.On , PowerValue.Off, PowerValue.Off, PowerValue.Off, PowerValue.Off, PowerValue.Off, PowerValue.Off) },
					{ '.', () => SetPowerValues(PowerValue.Off, PowerValue.Off, PowerValue.Off, PowerValue.Off, PowerValue.Off, PowerValue.Off, PowerValue.Off, PowerValue.On ) },
					{ '/', () => SetPowerValues(PowerValue.On , PowerValue.Off, PowerValue.Off, PowerValue.On , PowerValue.On , PowerValue.Off, PowerValue.Off) },

					{ '0', () => SetPowerValues(PowerValue.Off, PowerValue.On , PowerValue.On , PowerValue.On , PowerValue.On , PowerValue.On , PowerValue.On ) },
					{ '1', () => SetPowerValues(PowerValue.Off, PowerValue.Off, PowerValue.Off, PowerValue.On , PowerValue.Off, PowerValue.Off, PowerValue.On ) },
					{ '2', () => SetPowerValues(PowerValue.On , PowerValue.Off, PowerValue.On , PowerValue.On , PowerValue.On , PowerValue.On , PowerValue.Off) },
					{ '3', () => SetPowerValues(PowerValue.On , PowerValue.Off, PowerValue.On , PowerValue.On , PowerValue.Off, PowerValue.On , PowerValue.On ) },
					{ '4', () => SetPowerValues(PowerValue.On , PowerValue.On , PowerValue.Off, PowerValue.On , PowerValue.Off, PowerValue.Off, PowerValue.On ) },
					{ '5', () => SetPowerValues(PowerValue.On , PowerValue.On , PowerValue.On , PowerValue.Off, PowerValue.Off, PowerValue.On , PowerValue.On ) },
					{ '6', () => SetPowerValues(PowerValue.On , PowerValue.On , PowerValue.On , PowerValue.Off, PowerValue.On , PowerValue.On , PowerValue.On ) },
					{ '7', () => SetPowerValues(PowerValue.Off, PowerValue.Off, PowerValue.On , PowerValue.On , PowerValue.Off, PowerValue.Off, PowerValue.On ) },
					{ '8', () => SetPowerValues(PowerValue.On , PowerValue.On , PowerValue.On , PowerValue.On , PowerValue.On , PowerValue.On , PowerValue.On ) },
					{ '9', () => SetPowerValues(PowerValue.On , PowerValue.On , PowerValue.On , PowerValue.On , PowerValue.Off, PowerValue.On , PowerValue.On ) },

					{ '<', () => _mappings?['c']() },
					{ '=', () => SetPowerValues(PowerValue.On , PowerValue.Off, PowerValue.Off, PowerValue.Off, PowerValue.Off, PowerValue.On , PowerValue.Off) },
					{ '>', () => SetPowerValues(PowerValue.On , PowerValue.Off, PowerValue.Off, PowerValue.Off, PowerValue.Off, PowerValue.On , PowerValue.On ) },

					{ 'A', () => SetPowerValues(PowerValue.On , PowerValue.On , PowerValue.On , PowerValue.On , PowerValue.On , PowerValue.Off, PowerValue.On ) },
					{ 'B', () => _mappings?['8']() },
					{ 'C', () => SetPowerValues(PowerValue.Off, PowerValue.On , PowerValue.On , PowerValue.Off, PowerValue.On , PowerValue.On , PowerValue.Off) },
					{ 'D', () => _mappings?['0']() },
					{ 'E', () => SetPowerValues(PowerValue.On , PowerValue.On , PowerValue.On , PowerValue.Off, PowerValue.On , PowerValue.On , PowerValue.Off) },
					{ 'F', () => SetPowerValues(PowerValue.On , PowerValue.On , PowerValue.On , PowerValue.Off, PowerValue.On , PowerValue.Off, PowerValue.Off) },
					{ 'G', () => SetPowerValues(PowerValue.Off, PowerValue.On , PowerValue.On , PowerValue.Off, PowerValue.On , PowerValue.On , PowerValue.On ) },
					{ 'H', () => SetPowerValues(PowerValue.On , PowerValue.On , PowerValue.Off, PowerValue.On , PowerValue.On , PowerValue.Off, PowerValue.On ) },
					{ 'I', () => SetPowerValues(PowerValue.Off, PowerValue.On , PowerValue.Off, PowerValue.Off , PowerValue.On , PowerValue.Off, PowerValue.Off) },
					{ 'J', () => SetPowerValues(PowerValue.Off, PowerValue.Off, PowerValue.Off, PowerValue.On , PowerValue.On , PowerValue.On , PowerValue.On ) },
					{ 'K', () => _mappings?['H']() },
					{ 'L', () => SetPowerValues(PowerValue.Off, PowerValue.On , PowerValue.Off, PowerValue.Off, PowerValue.On , PowerValue.On , PowerValue.Off) },
					{ 'M', () => SetPowerValues(PowerValue.Off, PowerValue.On , PowerValue.On , PowerValue.On , PowerValue.On , PowerValue.Off, PowerValue.On ) },
					{ 'N', () => _mappings?['H']() },
					{ 'O', () => _mappings?['0']() },
					{ 'P', () => SetPowerValues(PowerValue.On , PowerValue.On , PowerValue.On , PowerValue.On , PowerValue.On , PowerValue.Off, PowerValue.Off) },
					{ 'Q', () => SetPowerValues(PowerValue.Off, PowerValue.On , PowerValue.On , PowerValue.On , PowerValue.On , PowerValue.On , PowerValue.On , PowerValue.On ) },
					{ 'R', () => _mappings?['A']() },
					{ 'S', () => _mappings?['5']() },
					{ 'T', () => SetPowerValues(PowerValue.Off, PowerValue.On , PowerValue.On , PowerValue.Off, PowerValue.On , PowerValue.Off, PowerValue.Off) },
					{ 'U', () => SetPowerValues(PowerValue.Off, PowerValue.On , PowerValue.Off, PowerValue.On , PowerValue.On , PowerValue.On , PowerValue.On ) },
					{ 'V', () => SetPowerValues(PowerValue.On , PowerValue.On , PowerValue.Off, PowerValue.On , PowerValue.On , PowerValue.Off, PowerValue.Off) },
					{ 'W', () => _mappings?['U']() },
					{ 'X', () => _mappings?['H']() },
					{ 'Y', () => _mappings?['4']() },
					{ 'Z', () => _mappings?['2']() },

					{ '[', () => _mappings?['(']() },
					{ '\\',() => SetPowerValues(PowerValue.On , PowerValue.On , PowerValue.Off, PowerValue.Off, PowerValue.Off, PowerValue.Off, PowerValue.On ) },
					{ ']', () => _mappings?[')']() },
					{ '^', () => SetPowerValues(PowerValue.Off, PowerValue.On , PowerValue.On , PowerValue.On , PowerValue.Off, PowerValue.Off, PowerValue.Off) },
					{ '_', () => SetPowerValues(PowerValue.Off, PowerValue.Off, PowerValue.Off, PowerValue.Off, PowerValue.Off, PowerValue.On , PowerValue.Off) },
					{ '`', () => SetPowerValues(PowerValue.Off, PowerValue.On , PowerValue.Off, PowerValue.Off, PowerValue.Off, PowerValue.Off, PowerValue.Off) },

					{ 'a', () => SetPowerValues(PowerValue.On , PowerValue.Off, PowerValue.On , PowerValue.On , PowerValue.On , PowerValue.On , PowerValue.On ) },
					{ 'b', () => SetPowerValues(PowerValue.On , PowerValue.On , PowerValue.Off, PowerValue.Off, PowerValue.On , PowerValue.On , PowerValue.On ) },
					{ 'c', () => SetPowerValues(PowerValue.On , PowerValue.Off, PowerValue.Off, PowerValue.Off, PowerValue.On , PowerValue.On , PowerValue.Off) },
					{ 'd', () => SetPowerValues(PowerValue.On , PowerValue.Off, PowerValue.Off, PowerValue.On , PowerValue.On , PowerValue.On , PowerValue.On ) },
					{ 'e', () => SetPowerValues(PowerValue.On , PowerValue.On , PowerValue.On , PowerValue.On , PowerValue.On , PowerValue.On , PowerValue.Off ) },
					{ 'f', () => _mappings?['F']() },
					{ 'g', () => _mappings?['9']() },
					{ 'h', () => SetPowerValues(PowerValue.On , PowerValue.On , PowerValue.Off, PowerValue.Off, PowerValue.On , PowerValue.Off, PowerValue.On ) },
					{ 'i', () => SetPowerValues(PowerValue.Off, PowerValue.Off, PowerValue.Off, PowerValue.Off, PowerValue.On , PowerValue.Off, PowerValue.Off) },
					{ 'j', () => SetPowerValues(PowerValue.Off, PowerValue.Off, PowerValue.Off, PowerValue.Off, PowerValue.Off, PowerValue.On , PowerValue.On ) },
					{ 'k', () => _mappings?['h']() },
					{ 'l', () => _mappings?['I']() },
					{ 'm', () => SetPowerValues(PowerValue.On , PowerValue.Off, PowerValue.Off, PowerValue.Off, PowerValue.On , PowerValue.Off, PowerValue.On ) },
					{ 'n', () => _mappings?['m']() },
					{ 'o', () => SetPowerValues(PowerValue.On , PowerValue.Off, PowerValue.Off, PowerValue.Off, PowerValue.On , PowerValue.On , PowerValue.On ) },
					{ 'p', () => _mappings?['P']() },
					{ 'q', () => SetPowerValues(PowerValue.On , PowerValue.On , PowerValue.On , PowerValue.On , PowerValue.Off, PowerValue.Off, PowerValue.On ) },
					{ 'r', () => SetPowerValues(PowerValue.On , PowerValue.Off, PowerValue.Off, PowerValue.Off, PowerValue.On , PowerValue.Off, PowerValue.Off) },
					{ 's', () => _mappings?['S']() },
					{ 't', () => SetPowerValues(PowerValue.On , PowerValue.On , PowerValue.Off, PowerValue.Off, PowerValue.On , PowerValue.On , PowerValue.Off) },
					{ 'u', () => SetPowerValues(PowerValue.Off, PowerValue.Off, PowerValue.Off, PowerValue.Off, PowerValue.On , PowerValue.On , PowerValue.On ) },
					{ 'v', () => _mappings?['u']() },
					{ 'w', () => _mappings?['u']() },
					{ 'x', () => _mappings?['X']() },
					{ 'y', () => _mappings?['Y']() },
					{ 'z', () => _mappings?['Z']() },

					{ '{', () => _mappings?['(']() },
					{ '|', () => _mappings?['I']() },
					{ '}', () => _mappings?[')']() },
					{ '~', () => SetPowerValues(PowerValue.Off, PowerValue.Off, PowerValue.On , PowerValue.Off, PowerValue.Off, PowerValue.Off, PowerValue.Off) },
				};
		}
	}
}