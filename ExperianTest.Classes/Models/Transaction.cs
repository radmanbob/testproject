using ExperianTest.Services;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ExperianTest.Models
{
    public class Transaction
    {
        public Transaction()
        {
            ChangeCalculated = new Change();
        }

        [Required]
        [DisplayName("Transaction cost £")]
        [DataType(DataType.Currency),]
        [Range(0.01, 999999.99, ErrorMessage = "Please enter a valid amount greater than £0.00 and less than or equal to £999999.99")]
        [RegularExpression(@"^[0-9]+((\.[0-9]{0,2})|())$", ErrorMessage = "Please enter a valid decimal number with maximum 2 decimal places.")]
        public decimal Cost { get; set; }

        [Required]
        [DisplayName("Tendered amount £")]
        [DataType(DataType.Currency)]
        [Range(0.01, 999999.99, ErrorMessage = "Please enter a valid amount greater than £0.00 and less than or equal to £999999.99")]
        [RegularExpression(@"^[0-9]+((\.[0-9]{0,2})|())$", ErrorMessage = "Please enter a valid decimal number with maximum 2 decimal places.")]
        public decimal Tendered { get; set; }

        public Change ChangeCalculated { get; set; }
    }
}
