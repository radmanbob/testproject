using ExperianTest.Models;

namespace ExperianTest.Services
{
    public interface IChangeCalculator
    {
        ChangeDTO CalculateChange(decimal tendered, decimal cost);
        int CheckDenomination(ref int changeRemaining, int coinToCheck);
    }
}
