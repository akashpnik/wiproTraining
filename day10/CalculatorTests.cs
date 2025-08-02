using CalculatorLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CalculatorLib.Tests
{
    public class  CalculatorTests
    {
        private Calculator calc;

        [TestInitialize]
        public void Setup()
        {
            calc = new Calculator();
        }

        [TestMethod]
        public void Add_TwoNumbers_ReturnsCorrectSum()
        {
            double result = calc.Add(3, 3);
            Assert.AreEqual(6, result);
        }

        [TestMethod]
        public void Subtract_TwoNumbers_ReturnsCorrectDifference()
        {
            double result = calc.Subtract(10, 4);
            Assert.AreEqual(6, result);
        }

        [TestMethod]
        public void Multiply_TwoNumbers_ReturnsCorrectProduct()
        {
            double result = calc.Multiply(2, 3);
            Assert.AreEqual(6, result);
        }

        [TestMethod]
        public void Divide_TwoNumbers_ReturnsCorrectQuotient()
        {
            double result = calc.Divide(10, 2);
            Assert.AreEqual(5, result);
        }

        [TestMethod]
        public void Divide_ByZero_ThrowsException()
        {
            Assert.Throws<DivideByZeroException>(() => calc.Divide(10, 0));
        }

        [TestMethod]
        public void Add_WithZero_ReturnsSameNumber()
        {
            double result = calc.Add(0, 5);
            Assert.AreEqual(5, result);
        }
    }
}
