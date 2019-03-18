using InterestRates.Bands;
using InterestRates.CSV;
using InterestRates.Interest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InterestRatesTests.IntegrationTests
{
    [TestClass]
    public class InterestRatesIntegrationTests
    {
        [TestMethod]
        public void Should_Read_Bands_And_Return_Correct_Interest()
        {
            var readerFactory = new TextReaderFactory(@"IntegrationTests\test_data\bands.txt");
            var bandsReader = new CSVBandsReader(readerFactory, "|");
            using (var bandsCache = new BandsCache(bandsReader))
            {
                var interestRateReturner = new InterestRateReturner(bandsCache);
                var interestCalculator = new InterestCalculator(interestRateReturner);

                Assert.AreEqual(15.02m, interestCalculator.GetAmountOfInterest(1001));
            }
        }
    }
}
