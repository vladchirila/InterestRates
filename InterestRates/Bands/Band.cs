using System;

namespace InterestRates.Bands
{
    public class Band : IComparable<Band>
    {
        public decimal? LowerLimit;
        public decimal? UpperLimit;
        public decimal InterestRate;

        public Band(decimal? lowerLimit, decimal? upperLimit, decimal interestRate)
        {
            LowerLimit = lowerLimit;
            UpperLimit = upperLimit;
            InterestRate = interestRate;
        }

        public int CompareTo(Band other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            var lowerLimitComparison = Nullable.Compare(LowerLimit, other.LowerLimit);
            if (lowerLimitComparison != 0) return lowerLimitComparison;
            var upperLimitComparison = Nullable.Compare(UpperLimit, other.UpperLimit);
            if (upperLimitComparison != 0) return upperLimitComparison;
            return InterestRate.CompareTo(other.InterestRate);
        }

        public override string ToString()
        {
            return $"{LowerLimit} - <{UpperLimit} - {InterestRate * 100}%";
        }

        public override bool Equals(object other)
        {
            if (other is Band otherBand)
            {
                return this.CompareTo(otherBand) == 0;
            }

            return false;
        }
    }
}