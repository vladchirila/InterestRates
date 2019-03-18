using System.IO;

namespace InterestRates.CSV
{
    public class TextReaderFactory : ITextReaderFactory
    {
        private readonly string _fileName;

        public TextReaderFactory(string fileName)
        {
            _fileName = fileName;
        }

        public TextReader GetTextReader()
        {
            return File.OpenText(_fileName);
        }
    }
}