using System.Collections.Generic;
using InterestRates.Bands;
using InterestRates.Interest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace InterestRatesTests.Interest
{
    [TestClass]
    public class InterestRateReturnerTests
    {
        private Mock<IBandsCache> _bandsCacheMock;
        private InterestRateReturner _interestRateReturner;

        [TestInitialize]
        public void SetUp()
        {
            _bandsCacheMock = new Mock<IBandsCache>();
            _interestRateReturner = new InterestRateReturner(_bandsCacheMock.Object);
        }

        [TestMethod]
        public void Given_All_Bands_When_Getting_Interest_Rate_It_Returns_The_Correct_One()
        {
            var balance = 1001m;
            var expectedInterestRate = 0.015m;

            var bands = new List<Band>
            {
                new Band(null, 1000, 0.010m),
                new Band(1000, 5000, 0.015m),
                new Band(5000, 10000, 0.020m),
                new Band(10000, 50000, 0.025m),
                new Band(50000, null, 0.030m)
            };

            _bandsCacheMock.Setup(x => x.GetNewSavingsAccountBands()).Returns(
                new SortedSet<Band>(bands));

            var returnedInterestRate = _interestRateReturner.GetInterestRate(balance);

            Assert.AreEqual(expectedInterestRate, returnedInterestRate);
        }
    }
}
