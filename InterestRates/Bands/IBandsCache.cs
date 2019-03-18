using System;
using System.Collections.Generic;

namespace InterestRates.Bands
{
    public interface IBandsCache : IDisposable
    {
        SortedSet<Band> GetNewSavingsAccountBands();
    }
}