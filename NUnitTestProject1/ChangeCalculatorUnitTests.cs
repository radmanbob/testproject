using ExperianTest.Models;
using ExperianTest.Services;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace ExperianTest.Tests
{
    public class ChangeCalculatorUnitTests
    {
        private readonly IChangeCalculator _calculator;

        public ChangeCalculatorUnitTests()
        {
            _calculator = new ChangeCalculator();
        }

        [SetUp]
        public void Setup()
        {
        }
        
        [Test]
        [TestCase(250, 100, 2)]
        [TestCase(100, 250, 0)]
        [TestCase(50, 250, 0)]
        [TestCase(100, 50, 2)]
        [TestCase(100, 100, 1)]
        [TestCase(0, 100, 0)]
        [TestCase(100, 0, 0)]
        [TestCase(0, 0, 0)]
        public void PerformDenominationTests(int amountRemaining, int denomination, int expectedResult)
        {
            var result = _calculator.CheckDenomination(ref amountRemaining, denomination);

            result.Should().Be(expectedResult);
        }

        // Note: These tests do not isolate the CalculateChange method from the CheckDenomination method. 
        [Test, TestCaseSource("TransactionTestCases")]
        public void PerformTransactionTests(Transaction transaction, Dictionary<int, int> expectedResult, bool tooLarge)
        {
            if (tooLarge == false)
            {
                var result = _calculator.CalculateChange(transaction.Tendered, transaction.Cost);

                result.DenominationAmounts.Should().BeEquivalentTo(expectedResult);
            }
            else
            {
                // Ensure that an exception is raised if the tendered or cost amounts are too large
                Assert.Throws<Exception>(() => _calculator.CalculateChange(transaction.Tendered, transaction.Cost));
            }
        }

        static readonly object[] TransactionTestCases =
        {
           new object[] {
                new Transaction()
                {
                    Cost = 0.60M,
                    Tendered = 0.80M
                },
                new Dictionary<int, int>()
                {
                    { 20, 1 },
                },
                false
            },
           new object[] {
                new Transaction()
                {
                    Cost = 19.60M,
                    Tendered = 20.00M
                },
                new Dictionary<int, int>()
                {
                    { 20, 2 },
                },
                false
            },
            new object[] {
                new Transaction()
                {
                    Cost = 17.34M,
                    Tendered = 20.00M
                },
                new Dictionary<int, int>()
                {
                    { 200, 1 },
                    { 50, 1 },
                    { 10, 1 },
                    { 5, 1 },
                    { 1, 1 }
                },
                false
            },
            new object[] {
                new Transaction()
                {
                    Cost = 30.00M,
                    Tendered = 25.00M
                },
                new Dictionary<int, int>()
                {},
                false
            },
            new object[] {
                new Transaction()
                {
                    Cost = 30.00M,
                    Tendered = 30.00M
                },
                new Dictionary<int, int>()
                {},
                false
            },
            new object[] {
                new Transaction()
                {
                    Cost = Convert.ToDecimal(Int32.MaxValue),
                    Tendered = 30.00M
                },
                new Dictionary<int, int>()
                {},
                true
            },
            new object[] {
                new Transaction()
                {
                    Cost = 50.00M,
                    Tendered = Convert.ToDecimal(Int32.MaxValue),
                },
                new Dictionary<int, int>()
                {},
                true
            },
        };
    }
}