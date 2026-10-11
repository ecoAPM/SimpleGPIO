using System.Collections;
using SimpleGPIO.Components;
using SimpleGPIO.Power;
using SimpleGPIO.Tests.GPIO;
using Xunit;

namespace SimpleGPIO.Tests.Components;

public sealed class SevenSegmentDisplayTests
{
	[Theory, ClassData(typeof(CharacterData))]
	public void TestCharacters(char character, string expected)
	{
		//arrange
		var segments = new SevenSegmentDisplay.PinSet
		{
			Center = new PinStub(1),
			UpperLeft = new PinStub(2),
			Top = new PinStub(3),
			UpperRight = new PinStub(4),
			LowerLeft = new PinStub(5),
			Bottom = new PinStub(6),
			LowerRight = new PinStub(7),
			Decimal = new PinStub(8)
		};
		var display = new SevenSegmentDisplay(segments);

		//act
		display.Show(character);

		//assert
		Assert.Equal(expected[1] == '*' ? PowerValue.On : PowerValue.Off, segments.Top.Power);
		Assert.Equal(expected[3] == '*' ? PowerValue.On : PowerValue.Off, segments.UpperLeft.Power);
		Assert.Equal(expected[5] == '*' ? PowerValue.On : PowerValue.Off, segments.UpperRight.Power);
		Assert.Equal(expected[7] == '*' ? PowerValue.On : PowerValue.Off, segments.Center.Power);
		Assert.Equal(expected[9] == '*' ? PowerValue.On : PowerValue.Off, segments.LowerLeft.Power);
		Assert.Equal(expected[11] == '*' ? PowerValue.On : PowerValue.Off, segments.LowerRight.Power);
		Assert.Equal(expected[13] == '*' ? PowerValue.On : PowerValue.Off, segments.Bottom.Power);

		if (expected.Length > 15)
			Assert.Equal(expected[15] == '*' ? PowerValue.On : PowerValue.Off, segments.Decimal.Power);
	}

	private sealed class CharacterData : IEnumerable<object[]>
	{
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		public IEnumerator<object[]> GetEnumerator()
		{
			yield return
			[
				' ', "   " +
				     "   " +
				     "   " +
				     "   " +
				     "   "
			];

			yield return
			[
				'"', "   " +
				     "* *" +
				     "   " +
				     "   " +
				     "   "
			];

			yield return
			[
				'\'', "   " +
				      "  *" +
				      "   " +
				      "   " +
				      "   "
			];

			yield return
			[
				'(', " * " +
				     "*  " +
				     "   " +
				     "*  " +
				     " * "
			];

			yield return
			[
				')', " * " +
				     "  *" +
				     "   " +
				     "  *" +
				     " * "
			];

			yield return
			[
				',', "   " +
				     "   " +
				     "   " +
				     "  *" +
				     "   "
			];

			yield return
			[
				'-', "   " +
				     "   " +
				     " * " +
				     "   " +
				     "   "
			];

			yield return
			[
				'.', "   " +
				     "   " +
				     "   " +
				     "   " +
				     "   *"
			];

			yield return
			[
				'/', "   " +
				     "  *" +
				     " * " +
				     "*  " +
				     "   "
			];

			yield return
			[
				'0', " * " +
				     "* *" +
				     "   " +
				     "* *" +
				     " * "
			];

			yield return
			[
				'1', "   " +
				     "  *" +
				     "   " +
				     "  *" +
				     "   "
			];

			yield return
			[
				'2', " * " +
				     "  *" +
				     " * " +
				     "*  " +
				     " * "
			];

			yield return
			[
				'3', " * " +
				     "  *" +
				     " * " +
				     "  *" +
				     " * "
			];

			yield return
			[
				'4', "   " +
				     "* *" +
				     " * " +
				     "  *" +
				     "   "
			];

			yield return
			[
				'5', " * " +
				     "*  " +
				     " * " +
				     "  *" +
				     " * "
			];

			yield return
			[
				'6', " * " +
				     "*  " +
				     " * " +
				     "* *" +
				     " * "
			];

			yield return
			[
				'7', " * " +
				     "  *" +
				     "   " +
				     "  *" +
				     "   "
			];

			yield return
			[
				'8', " * " +
				     "* *" +
				     " * " +
				     "* *" +
				     " * "
			];

			yield return
			[
				'9', " * " +
				     "* *" +
				     " * " +
				     "  *" +
				     " * "
			];

			yield return
			[
				'<', "   " +
				     "   " +
				     " * " +
				     "*  " +
				     " * "
			];

			yield return
			[
				'=', "   " +
				     "   " +
				     " * " +
				     "   " +
				     " * "
			];

			yield return
			[
				'>', "   " +
				     "   " +
				     " * " +
				     "  *" +
				     " * "
			];

			yield return
			[
				'A', " * " +
				     "* *" +
				     " * " +
				     "* *" +
				     "   "
			];

			yield return
			[
				'B', " * " +
				     "* *" +
				     " * " +
				     "* *" +
				     " * "
			];

			yield return
			[
				'C', " * " +
				     "*  " +
				     "   " +
				     "*  " +
				     " * "
			];

			yield return
			[
				'D', " * " +
				     "* *" +
				     "   " +
				     "* *" +
				     " * "
			];

			yield return
			[
				'E', " * " +
				     "*  " +
				     " * " +
				     "*  " +
				     " * "
			];

			yield return
			[
				'F', " * " +
				     "*  " +
				     " * " +
				     "*  " +
				     "   "
			];

			yield return
			[
				'G', " * " +
				     "*  " +
				     "   " +
				     "* *" +
				     " * "
			];

			yield return
			[
				'H', "   " +
				     "* *" +
				     " * " +
				     "* *" +
				     "   "
			];

			yield return
			[
				'I', "   " +
				     "*  " +
				     "   " +
				     "*  " +
				     "   "
			];

			yield return
			[
				'J', "   " +
				     "  *" +
				     "   " +
				     "* *" +
				     " * "
			];

			yield return
			[
				'K', "   " +
				     "* *" +
				     " * " +
				     "* *" +
				     "   "
			];

			yield return
			[
				'L', "   " +
				     "*  " +
				     "   " +
				     "*  " +
				     " * "
			];

			yield return
			[
				'M', " * " +
				     "* *" +
				     "   " +
				     "* *" +
				     "   "
			];

			yield return
			[
				'N', "   " +
				     "* *" +
				     " * " +
				     "* *" +
				     "   "
			];

			yield return
			[
				'O', " * " +
				     "* *" +
				     "   " +
				     "* *" +
				     " * "
			];

			yield return
			[
				'P', " * " +
				     "* *" +
				     " * " +
				     "*  " +
				     "   "
			];

			yield return
			[
				'Q', " * " +
				     "* *" +
				     "   " +
				     "* *" +
				     " * *"
			];

			yield return
			[
				'R', " * " +
				     "* *" +
				     " * " +
				     "* *" +
				     "   "
			];

			yield return
			[
				'S', " * " +
				     "*  " +
				     " * " +
				     "  *" +
				     " * "
			];

			yield return
			[
				'T', " * " +
				     "*  " +
				     "   " +
				     "*  " +
				     "   "
			];

			yield return
			[
				'U', "   " +
				     "* *" +
				     "   " +
				     "* *" +
				     " * "
			];

			yield return
			[
				'V', "   " +
				     "* *" +
				     " * " +
				     "*  " +
				     "   "
			];

			yield return
			[
				'W', "   " +
				     "* *" +
				     "   " +
				     "* *" +
				     " * "
			];

			yield return
			[
				'X', "   " +
				     "* *" +
				     " * " +
				     "* *" +
				     "   "
			];

			yield return
			[
				'Y', "   " +
				     "* *" +
				     " * " +
				     "  *" +
				     "   "
			];

			yield return
			[
				'Z', " * " +
				     "  *" +
				     " * " +
				     "*  " +
				     " * "
			];

			yield return
			[
				'[', " * " +
				     "*  " +
				     "   " +
				     "*  " +
				     " * "
			];

			yield return
			[
				'\\', "   " +
				      "*  " +
				      " * " +
				      "  *" +
				      "   "
			];

			yield return
			[
				']', " * " +
				     "  *" +
				     "   " +
				     "  *" +
				     " * "
			];

			yield return
			[
				'^', " * " +
				     "* *" +
				     "   " +
				     "   " +
				     "   "
			];

			yield return
			[
				'_', "   " +
				     "   " +
				     "   " +
				     "   " +
				     " * "
			];

			yield return
			[
				'`', "   " +
				     "*  " +
				     "   " +
				     "   " +
				     "   "
			];

			yield return
			[
				'a', " * " +
				     "  *" +
				     " * " +
				     "* *" +
				     " * "
			];

			yield return
			[
				'b', "   " +
				     "*  " +
				     " * " +
				     "* *" +
				     " * "
			];

			yield return
			[
				'c', "   " +
				     "   " +
				     " * " +
				     "*  " +
				     " * "
			];

			yield return
			[
				'd', "   " +
				     "  *" +
				     " * " +
				     "* *" +
				     " * "
			];

			yield return
			[
				'e', " * " +
				     "* *" +
				     " * " +
				     "*  " +
				     " * "
			];

			yield return
			[
				'f', " * " +
				     "*  " +
				     " * " +
				     "*  " +
				     "   "
			];

			yield return
			[
				'g', " * " +
				     "* *" +
				     " * " +
				     "  *" +
				     " * "
			];

			yield return
			[
				'h', "   " +
				     "*  " +
				     " * " +
				     "* *" +
				     "   "
			];

			yield return
			[
				'i', "   " +
				     "   " +
				     "   " +
				     "*  " +
				     "   "
			];

			yield return
			[
				'j', "   " +
				     "   " +
				     "   " +
				     "  *" +
				     " * "
			];

			yield return
			[
				'k', "   " +
				     "*  " +
				     " * " +
				     "* *" +
				     "   "
			];

			yield return
			[
				'l', "   " +
				     "*  " +
				     "   " +
				     "*  " +
				     "   "
			];

			yield return
			[
				'm', "   " +
				     "   " +
				     " * " +
				     "* *" +
				     "   "
			];

			yield return
			[
				'n', "   " +
				     "   " +
				     " * " +
				     "* *" +
				     "   "
			];

			yield return
			[
				'o', "   " +
				     "   " +
				     " * " +
				     "* *" +
				     " * "
			];

			yield return
			[
				'p', " * " +
				     "* *" +
				     " * " +
				     "*  " +
				     "   "
			];

			yield return
			[
				'q', " * " +
				     "* *" +
				     " * " +
				     "  *" +
				     "   "
			];

			yield return
			[
				'r', "   " +
				     "   " +
				     " * " +
				     "*  " +
				     "   "
			];

			yield return
			[
				's', " * " +
				     "*  " +
				     " * " +
				     "  *" +
				     " * "
			];

			yield return
			[
				't', "   " +
				     "*  " +
				     " * " +
				     "*  " +
				     " * "
			];

			yield return
			[
				'u', "   " +
				     "   " +
				     "   " +
				     "* *" +
				     " * "
			];

			yield return
			[
				'v', "   " +
				     "   " +
				     "   " +
				     "* *" +
				     " * "
			];

			yield return
			[
				'w', "   " +
				     "   " +
				     "   " +
				     "* *" +
				     " * "
			];

			yield return
			[
				'x', "   " +
				     "* *" +
				     " * " +
				     "* *" +
				     "   "
			];

			yield return
			[
				'y', "   " +
				     "* *" +
				     " * " +
				     "  *" +
				     "   "
			];

			yield return
			[
				'z', " * " +
				     "  *" +
				     " * " +
				     "*  " +
				     " * "
			];

			yield return
			[
				'{', " * " +
				     "*  " +
				     "   " +
				     "*  " +
				     " * "
			];

			yield return
			[
				'|', "   " +
				     "*  " +
				     "   " +
				     "*  " +
				     "   "
			];

			yield return
			[
				'}', " * " +
				     "  *" +
				     "   " +
				     "  *" +
				     " * "
			];

			yield return
			[
				'~', " * " +
				     "   " +
				     "   " +
				     "   " +
				     "   "
			];
		}
	}
}