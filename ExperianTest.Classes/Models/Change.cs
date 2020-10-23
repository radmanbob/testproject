using System.Collections.Generic;

namespace ExperianTest.Services
{
    public class Change
    {
        public Change()
        {
            DenominationAmounts = new Dictionary<int, int>();
        }

        public Change(Dictionary<int, int> denominationAmounts)
        {
            DenominationAmounts = denominationAmounts;
        }

        public Dictionary<int, int> DenominationAmounts { get; set; }
    }
}
