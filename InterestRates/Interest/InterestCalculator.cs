using System;

namespace InterestRates.Interest
{
    public class InterestCalculator : IInterestCalculator
    {
        private readonly IInterestRateReturner _interestRateReturner;

        public InterestCalculator(IInterestRateReturner interestRateReturner)
        {
            _interestRateReturner = interestRateReturner;
        }

        public decimal GetAmountOfInterest(decimal balance)
        {
            var rate = _interestRateReturner.GetInterestRate(balance);

            return Math.Round(rate * balance, 2, MidpointRounding.AwayFromZero);
        }
    }
}
