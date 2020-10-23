using ExperianTest.Models;
using ExperianTest.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExperianTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly IChangeCalculator _changeCalculator;
        public HomeController(
            IChangeCalculator changeCalculator)
        {
            _changeCalculator = changeCalculator;
        }

        /// <summary>
        /// Default page load
        /// </summary>
        /// <returns>View containing the form fields</returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Calculates the change from the transaction cost and amount paid
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns>View containing the form fields and change amount</returns>
        [HttpPost]
        public IActionResult Index(Transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            ChangeDTO changeDTO = _changeCalculator.CalculateChange(transaction.Tendered, transaction.Cost);

            // Simple use of a DTO object, just to demonstrate not passing service/business layer objects to the view layer.
            transaction.ChangeCalculated = new Change(changeDTO.DenominationAmounts);

            return View(transaction);
        }
    }
}
