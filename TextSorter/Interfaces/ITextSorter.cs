namespace TextSorter.Interfaces;

public interface ITextSorter
{
    IEnumerable<string> Sort(string paragraph);
}