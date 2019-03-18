namespace InterestRates.Interest
{
    public interface IInterestRateReturner
    {
        decimal GetInterestRate(decimal balance);
    }
}
