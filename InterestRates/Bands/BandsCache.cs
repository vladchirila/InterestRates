using System.Collections.Generic;

namespace InterestRates.Bands
{
    public class BandsCache : IBandsCache
    {
        private readonly IBandsReader _bandsReader;
        private SortedSet<Band> _bands;

        public BandsCache(IBandsReader bandsReader)
        {
            _bandsReader = bandsReader;
        }

        public SortedSet<Band> GetNewSavingsAccountBands()
        {
            if (_bands == null)
                lock (_bandsReader)
                    if (_bands == null)
                        _bands = new SortedSet<Band>(_bandsReader.GetNewSavingsAccountBands());
            return _bands;
        }

        public void Dispose()
        {
            _bandsReader.Dispose();
        }
    }
}