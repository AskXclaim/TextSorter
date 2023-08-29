namespace TextSorter;

public class TextSorter : ITextSorter
{
    private readonly ITextSorterLogger _logger;
    public TextSorter(ITextSorterLogger logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// A method to sort texts using the following rules
    /// words would be reordered Alphabetically - (Zebra Abba) becomes (Abba Zebra)
    /// words would THEN be ordered from upper case to lower case.
    /// Note point 1 takes preference. (aBba Abba) becomes (Abba aBba)
    /// Remove all (.,;') chars. (aBba, Abba) becomes (Abba aBba)
    /// Note that it's assumed that no words will contain the above characters as part of the word
    /// </summary>
    /// <param name="paragraph"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public IEnumerable<string> Sort(string paragraph)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(paragraph))
            {
                const string errorMsg = $"{nameof(paragraph)} cannot be null empty or whitespace";
                _logger.Log(errorMsg);
                throw new ArgumentNullException(errorMsg);
            }

            paragraph = RemoveUnwantedCharacters(paragraph.Trim());
            paragraph = RemoveExtraSpaces(paragraph);
            var orderedWords = SortWords(paragraph);

            return orderedWords.ToList();
        }
        catch (Exception exception)
        {
            _logger.Log(exception.Message);
            throw;
        }
     
    }

    private string[] SortWords(string paragraph)
    {
        var split = paragraph.Split(" ");
        var orderedWords = split.OrderBy(x => x, StringComparer.Ordinal).ToArray();
        Array.Sort(orderedWords);

        return orderedWords;
    }

    private string RemoveUnwantedCharacters(string paragraph)
    {
        paragraph = paragraph.Replace(".", " ");
        paragraph = paragraph.Replace(",", " ");
        paragraph = paragraph.Replace(";", " ");
        paragraph = paragraph.Replace("'", " ");
        return paragraph.Replace("`", " ");
    }

    private string RemoveExtraSpaces(string paragraph)
    {
        var options = RegexOptions.None;
        var regex = new Regex("[ ]{2,}", options);
        return regex.Replace(paragraph, " ");
    }
}