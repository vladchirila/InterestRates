using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using InterestRates.Bands;

namespace InterestRates.CSV
{
    public class CSVBandsReader : IBandsReader
    {
        private readonly TextReader _textReader;
        private readonly string[] _columnSeparator;

        public CSVBandsReader(ITextReaderFactory textReader, string columnSeparator)
        {
            _textReader = textReader.GetTextReader();
            _columnSeparator = new[] { columnSeparator };
        }

        public List<Band> GetNewSavingsAccountBands()
        {
            var bandList = new List<Band>();

            string[] columns;
            while ((columns = Read()) != null)
            {
                if (columns.Length != 3)
                    throw new Exception("Line does not have 3 columns.");

                var lowerLimitString = columns[0];
                var upperLimitString = columns[1];
                var interestRateString = columns[2];
                
                decimal? lowerLimit = null;
                if (lowerLimitString != string.Empty)
                    lowerLimit = decimal.Parse(lowerLimitString, CultureInfo.InvariantCulture);

                decimal? upperLimit = null;
                if (upperLimitString != string.Empty)
                    upperLimit = decimal.Parse(upperLimitString, CultureInfo.InvariantCulture);

                var interestRate = decimal.Parse(interestRateString, CultureInfo.InvariantCulture);

                bandList.Add(new Band(lowerLimit, upperLimit, interestRate));
            }

            return bandList;
        }

        private string[] Read()
        {
            var line = _textReader.ReadLine();

            var columns = line?.Split(_columnSeparator, StringSplitOptions.None);

            return columns;
        }

        public void Dispose()
        {
            _textReader.Dispose();
        }
    }
}