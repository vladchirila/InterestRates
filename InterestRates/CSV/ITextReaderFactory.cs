using System.IO;

namespace InterestRates.CSV
{
    public interface ITextReaderFactory
    {
        TextReader GetTextReader();
    }
}