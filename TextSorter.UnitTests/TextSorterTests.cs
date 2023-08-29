namespace TextSorter.UnitTests;

public class TextSorterTests
{
    private readonly Mock<ITextSorterLogger> _logger = new Mock<ITextSorterLogger>();

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Sort_WhenCalledWithNoContent_ThrowsArgumentNullException(string paragraph)
    {
        var sut = new TextSorter(_logger.Object);

        Assert.Throws<ArgumentNullException>(() => sut.Sort(paragraph));

        _logger.Verify(l =>
            l.Log($"{nameof(paragraph)} cannot be null empty or whitespace"), Times.Once);
    }

    [Theory]
    [InlineData("Happy ahappy   AHappy", "ahappy AHappy Happy")]
    [InlineData("Happy ahappy AHappy", "ahappy AHappy Happy")]
    [InlineData("Happy AHappy ahappy ", "ahappy AHappy Happy")]
    [InlineData("Zebra   Abba", "Abba Zebra")]
    [InlineData("Abba Zebra", "Abba Zebra")]
    [InlineData("aBba Abba", "aBba Abba")]
    [InlineData("Abba aBba", "aBba Abba")]
    public void Sort_WhenCalledWithValidContent_ReturnsExpectedResult
        (string paragraph, string expectedResult)
    {
        var sut = new TextSorter(_logger.Object);

        var content = sut.Sort(paragraph);

        Assert.Equal(content, expectedResult.Split());
    }
}