using Xunit;

namespace SimpleGPIO.Tests;

public class MathTests
{
	[Theory]
	[InlineData(1, 2, 4, 2)]
	[InlineData(2, 2, 4, 2)]
	[InlineData(3, 2, 4, 3)]
	[InlineData(4, 2, 4, 4)]
	[InlineData(5, 2, 4, 4)]
	public void CanClampNumber(int value, int min, int max, int expected)
	{
		//act
		var actual = value.Clamp(min, max);

		//assert
		Assert.Equal(expected, actual);
	}
}