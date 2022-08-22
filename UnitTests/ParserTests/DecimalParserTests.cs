using TransactionStorage.InputParser;

namespace UnitTests.ParserTests
{
    public class DecimalParserTests
    {
        [Test]
        public void TryParse_WhenInputIsDecimal_ReturnsTrue()
        {
            // Assert
            var sut = new DecimalParser();

            // Act
            var result = sut.TryParse("5.01", out _);

            // Assert
            result.Should().BeTrue();
        }

        [Test]
        public void TryParse_WhenInputIsNotDecimal_ReturnsFalse()
        {
            // Assert
            var sut = new DecimalParser();

            // Act
            var result = sut.TryParse("5 01,1", out _);

            // Assert
            result.Should().BeFalse();
        }

        [Test]
        public void TryParse_WhenAbleToParse_ConvertsStringToDecimal()
        {
            // Assert
            var sut = new DecimalParser();

            // Act
            _ = sut.TryParse("5.01", out var result);

            // Assert
            result.Should().Be(5.01m);
        }
    }
}