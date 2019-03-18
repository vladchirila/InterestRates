using InterestRates.Interest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace InterestRatesTests.Interest
{
    [TestClass]
    public class InterestCalculatorTests
    {
        private Mock<IInterestRateReturner> _interestRateMock;
        private InterestCalculator _interestCalculator;

        [TestInitialize]
        public void SetUp()
        {
            _interestRateMock = new Mock<IInterestRateReturner>();
            _interestCalculator = new InterestCalculator(_interestRateMock.Object);
        }

        [DataTestMethod]
        [DataRow(100100, 15, 1502)]
        [DataRow(100300, 15, 1505)]
        [DataRow(1000, 0, 0)]
        public void Given_Balance_And_Interest_Rate_When_Calculating_Interest_Rate_Then_We_Get_Expected_Value(int balanceInPence, int interestRateMultipliedByThousand, int expectedInterestAmountInPence)
        {
            //The reason I'm using ints here and then converting them to decimals is https://stackoverflow.com/questions/2763981/an-attribute-argument-must-be-a-constant-expression
            var interestRate = interestRateMultipliedByThousand / 1000m;
            var balance = balanceInPence / 100m;
            var expectedInterestAmount = expectedInterestAmountInPence / 100m;

            _interestRateMock.Setup(x => x.GetInterestRate(It.IsAny<decimal>())).Returns(interestRate);

            var interestAmount = _interestCalculator.GetAmountOfInterest(balance);

            Assert.AreEqual(expectedInterestAmount, interestAmount);
        }
    }
}
