using ExperianTest.Controllers;
using ExperianTest.Models;
using ExperianTest.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace ExperianTest.Tests
{
    public class HomeControllerUnitTests
    {
        private readonly Mock<IChangeCalculator> _mockCalculator;
        private readonly HomeController _controller;

        public HomeControllerUnitTests()
        {
            _mockCalculator = new Mock<IChangeCalculator>();
            _mockCalculator
                .Setup(mock => mock.CalculateChange(It.IsAny<decimal>(), It.IsAny<decimal>()))
                .Returns(new ChangeDTO());

            _controller = new HomeController(_mockCalculator.Object);
        }

        [SetUp]
        public void Setup()
        {
        }
        
        [Test]
        public void WhenIndexPostIsCalledThenCalculateChangeIsCalled()
        {
            IActionResult actionResult = _controller.Index(new Transaction());

            _mockCalculator.Verify(mock => mock.CalculateChange(It.IsAny<decimal>(), It.IsAny<decimal>()), Times.Once());
        }

        [Test]
        public void WhenIndexGetIsCalledThenCalculateChangeIsNotCalled()
        {
            IActionResult actionResult = _controller.Index();

            _mockCalculator.Verify(mock => mock.CalculateChange(It.IsAny<decimal>(), It.IsAny<decimal>()), Times.Never());
        }
    }
}