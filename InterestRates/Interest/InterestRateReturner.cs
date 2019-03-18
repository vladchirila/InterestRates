using System;
using InterestRates.Bands;

namespace InterestRates.Interest
{
    public class InterestRateReturner : IInterestRateReturner
    {
        private readonly IBandsCache _bandsCache;

        public InterestRateReturner(IBandsCache bandsCache)
        {
            _bandsCache = bandsCache;
        }

        public decimal GetInterestRate(decimal balance)
        {
            var bands = _bandsCache.GetNewSavingsAccountBands();

            foreach (var band in bands)
            {
                if ((band.LowerLimit == null || balance >= band.LowerLimit.Value) &&
                    (band.UpperLimit == null || balance < band.UpperLimit.Value))
                    return band.InterestRate;
            }

            throw new Exception("No band found for specified balance.");
        }
    }
}