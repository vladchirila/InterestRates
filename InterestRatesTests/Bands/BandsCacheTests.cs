using System.Collections.Generic;
using InterestRates.Bands;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace InterestRatesTests.Bands
{
    [TestClass]
    public class BandsCacheTests
    {
        private BandsCache _bandsCache;
        private Mock<IBandsReader> _bandsReader;

        [TestInitialize]
        public void SetUp()
        {
            _bandsReader = new Mock<IBandsReader>();
            _bandsCache = new BandsCache(_bandsReader.Object);
        }

        [TestMethod]
        public void When_Calling_Method_Multiple_Times_Then_It_Only_Calls_Reader_Once()
        {
            _bandsReader.Setup(x => x.GetNewSavingsAccountBands()).Returns(new List<Band>());

            _bandsCache.GetNewSavingsAccountBands();
            _bandsCache.GetNewSavingsAccountBands();

            _bandsReader.Verify(x => x.GetNewSavingsAccountBands(), Times.Once());
        }

        [TestMethod]
        public void Given_Reader_Bands_When_Getting_Cached_Bands_Then_They_Match()
        {
            var band1 = new Band(null, 1000, 1);
            var band2 = new Band(1000, null, 1);
            _bandsReader.Setup(x => x.GetNewSavingsAccountBands()).Returns(new List<Band> {band1, band2});

            var returnedBands = _bandsCache.GetNewSavingsAccountBands();

            Assert.IsTrue(returnedBands.Count == 2);
            Assert.IsTrue(returnedBands.Contains(band1));
            Assert.IsTrue(returnedBands.Contains(band2));
        }
    }
}
