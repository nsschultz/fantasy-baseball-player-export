using FantasyBaseball.Common.Enums;
using Xunit;

namespace FantasyBaseball.PlayerExportService.Converters.UnitTests
{
    public class PlayerTypeConverterTest
    {
        [Theory]
        [InlineData(     "b", PlayerType.B)]
        [InlineData(   " P ", PlayerType.P)]
        [InlineData("Batter", PlayerType.B)]
        [InlineData(      "", PlayerType.U)]
        [InlineData(    null, PlayerType.U)]
        public void ConvertFromStringTest(string value, PlayerType expected) => 
            Assert.Equal(expected, new PlayerTypeConverter().ConvertFromString(value, null, null));

        [Theory]
        [InlineData(PlayerType.B, "B")]
        [InlineData(        null, "U")]
        public void ConvertToStringTest(object value, string expected) => 
            Assert.Equal(expected, new PlayerTypeConverter().ConvertToString(value, null, null));
    }
}