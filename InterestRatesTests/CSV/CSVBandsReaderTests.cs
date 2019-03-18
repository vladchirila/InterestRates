using System.IO;
using InterestRates.Bands;
using InterestRates.CSV;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace InterestRatesTests.CSV
{
    [TestClass]
    public class CSVBandsReaderTests
    {
        private Mock<TextReader> _tr;
        private CSVBandsReader _csvBandsReader;

        [TestInitialize]
        public void SetUp()
        {
            var factory = new Mock<ITextReaderFactory>();
            _tr = new Mock<TextReader>();
            factory.Setup(x => x.GetTextReader()).Returns(_tr.Object);
            _csvBandsReader = new CSVBandsReader(factory.Object, "|");
        }

        [TestMethod]
        public void Given_Specified_Text_Lines_When_Getting_Bands_They_Match_The_Text_Lines()
        {
            var textLines = new[] {"|1000|0.010", "1000|5000|0.015", "5000|10000|0.020", "10000|50000|0.025", "50000||0.030"};
            var expectedBands = new[]
            {
                new Band(null, 1000m, 0.01m),
                new Band(1000m, 5000m, 0.015m),
                new Band(5000m, 10000m, 0.020m),
                new Band(10000m, 50000m, 0.025m),
                new Band(50000m, null, 0.030m),
            };

            var i = 0;
            _tr.Setup(x => x.ReadLine()).Returns(() => i < textLines.Length ? textLines[i++] : null);

            var bands = _csvBandsReader.GetNewSavingsAccountBands();

            Assert.AreEqual(expectedBands.Length, bands.Count);

            foreach (var band in bands)
            {
                if(!Contains(expectedBands, band))
                    Assert.Fail($"Band missing from expected bands: {band}");
            }
        }

        private bool Contains(Band[] expectedBands, Band band)
        {
            foreach (var expectedBand in expectedBands)
            {
                if (expectedBand.Equals(band))
                    return true;
            }
            return false;
        }
    }
}
