using System;
using System.Collections.Generic;

namespace InterestRates.Bands
{
    public interface IBandsReader : IDisposable
    {
        List<Band> GetNewSavingsAccountBands();
    }
}