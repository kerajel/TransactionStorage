using TransactionStorage.InputParser;

namespace UnitTests.ParserTests
{
    public class IntegerParserTests
    {
        [Test]
        public void TryParse_WhenInputIsInteger_ReturnsTrue()
        {
            // Assert
            var sut = new IntegerParser();

            // Act
            var result = sut.TryParse("5", out _);

            // Assert
            result.Should().BeTrue();
        }

        [Test]
        public void TryParse_WhenInputIsNotInteger_ReturnsFalse()
        {
            // Assert
            var sut = new IntegerParser();

            // Act
            var result = sut.TryParse("5X", out _);

            // Assert
            result.Should().BeFalse();
        }

        [Test]
        public void TryParse_WhenAbleToParse_ConvertsStringToInteger()
        {
            // Assert
            var sut = new IntegerParser();

            // Act
            _ = sut.TryParse("5", out var result);

            // Assert
            result.Should().Be(5);
        }

        [Test]
        public void TryParse_WhenInputIsLessThanZero_ReturnsFalse()
        {
            // Assert
            var sut = new IntegerParser();

            // Act
            var result = sut.TryParse("-5", out _);

            // Assert
            result.Should().BeFalse();
        }
    }
}