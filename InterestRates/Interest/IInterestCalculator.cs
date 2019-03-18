namespace InterestRates.Interest
{
    public interface IInterestCalculator
    {
        decimal GetAmountOfInterest(decimal balance);
    }
}