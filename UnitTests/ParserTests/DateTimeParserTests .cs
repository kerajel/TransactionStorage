using TransactionStorage.InputParser;
using TransactionStorage.Model;

namespace UnitTests.ParserTests
{
    public class DateTimeParserTests
    {
        [Test]
        public void TryParse_WhenInputIsDateTime_ReturnsTrue()
        {
            // Assert
            var sut = new DateTimeParser(new AppSettings() { DateTimeFormat = "dd.MM.yyyy" });

            // Act
            var result = sut.TryParse("15.02.2021", out _);

            // Assert
            result.Should().BeTrue();
        }

        [Test]
        public void TryParse_WhenInputIsNotDateTime_ReturnsFalse()
        {
            // Assert
            var sut = new DateTimeParser(new AppSettings() { DateTimeFormat = "dd.MM.yyyy" });

            // Act
            var result = sut.TryParse("Jan 16th", out _);

            // Assert
            result.Should().BeFalse();
        }

        [Test]
        public void TryParse_WhenAbleToParse_ConvertsStringToDateTime()
        {
            // Assert
            var sut = new DateTimeParser(new AppSettings() { DateTimeFormat = "dd.MM.yyyy" });

            // Act
            _ = sut.TryParse("25.05.2021", out var result);

            // Assert
            result.Should().Be(new DateTime(2021, 05, 25));
        }

        [Test]
        public void TryParse_WhenParsing_UsesDateTimeFormat()
        {
            // Assert
            var sut = new DateTimeParser(new AppSettings() { DateTimeFormat = "ddMMyyyy" });

            // Act
            var parse1 = sut.TryParse("25052021", out var result1);
            var parse2 = sut.TryParse("25.05.2021", out var result2);

            // Assert Positive
            parse1.Should().BeTrue();
            result1.Should().Be(new DateTime(2021, 05, 25));

            // Assert Negative
            parse2.Should().BeFalse();
            result2.Should().Be(default);
        }
    }
}