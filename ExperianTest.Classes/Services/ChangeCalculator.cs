using ExperianTest.Models;
using System;

namespace ExperianTest.Services
{
    public class ChangeCalculator : IChangeCalculator
    {
        public ChangeCalculator()
        {
        }

        /// <summary>
        /// Calculate the amount of change to give to a customer
        /// </summary>
        /// <param name="tendered">Amount tendered</param>
        /// <param name="cost">Cost of the purchase</param>
        /// <returns>Change to give</returns>
        public ChangeDTO CalculateChange(decimal tendered, decimal cost)
        {
            var denominationArray = new int[] { 5000, 2000, 1000, 500, 200, 100, 50, 20, 10, 5, 2, 1 };

            int tenderedAmount;
            int costAmount;

            int maxAmount = Int32.MaxValue / 100;

            // Ensure that the tendered amount and cost are not too large to convert to an int32
            if (maxAmount < tendered  || maxAmount < cost)
            {
                throw new ArgumentException("Tendered or Cost amounts are greater than maximum allowed."); 
            }
            
            tenderedAmount = Convert.ToInt32(tendered * 100);
            costAmount = Convert.ToInt32(cost * 100);

            var changeRemaining = tenderedAmount - costAmount;

            var change = new ChangeDTO();

            // Loop through the different note/coin denominations, calculating how many of each we need to give in change
            var i = 0;
            while (changeRemaining > 0)
            {
                var currentDenomination = denominationArray[i];

                var numberFound = CheckDenomination(ref changeRemaining, currentDenomination);

                if (numberFound > 0)
                {
                    change.DenominationAmounts.Add(currentDenomination, numberFound);
                } 

                i++;
            }

            return change;
        }

        /// <summary>
        /// Check to see how many of an individual demonination should be returned to the payee
        /// </summary>
        /// <param name="changeRemaining">How much change is left to give</param>
        /// <param name="denominationToCheck">Which denomination coin/note to check</param>
        /// <returns>Number of that denomination to give</returns>
        public int CheckDenomination(ref int changeRemaining, int denominationToCheck)
        {
            if (denominationToCheck > 0 && changeRemaining > 0)
            {
                var numberFound = changeRemaining / denominationToCheck;
                changeRemaining -= numberFound * denominationToCheck;
                return numberFound;
            }

            return 0;
        }
    }
}
