using System.Collections.Generic;

namespace ExperianTest.Services
{
    public class ChangeDTO
    {
        public ChangeDTO()
        {
            DenominationAmounts = new Dictionary<int, int>();
        }

        public Dictionary<int, int> DenominationAmounts { get; set; }
    }
}
