using CalculatorLib;
using NUnit.Framework;
using System;

namespace CalculatorLib.Tests
{
    public class CalculatorTests
    {
        private Calculator calc;

        [SetUp]
        public void Setup()
        {
            calc = new Calculator();
        }

        [Test]
        public void Add_TwoNumbers_ReturnsCorrectSum()
        {
            double result = calc.Add(3, 3);
            Assert.That(result, Is.EqualTo(6));
        }

        [Test]
        public void Subtract_TwoNumbers_ReturnsCorrectDifference()
        {
            double result = calc.Subtract(10, 4);
            Assert.That(result, Is.EqualTo(6));
        }

        [Test]
        public void Multiply_TwoNumbers_ReturnsCorrectProduct()
        {
            double result = calc.Multiply(2, 3);
            Assert.That(result, Is.EqualTo(6));
        }

        [Test]
        public void Divide_TwoNumbers_ReturnsCorrectQuotient()
        {
            double result = calc.Divide(10, 2);
            Assert.That(result, Is.EqualTo(5));
        }

        [Test]
        public void Divide_ByZero_ThrowsException()
        {
            Assert.Throws<DivideByZeroException>(() => calc.Divide(10, 0));
        }

        [Test]
        public void Add_WithZero_ReturnsSameNumber()
        {
            double result = calc.Add(0, 5);
            Assert.That(result, Is.EqualTo(5));
        }
    }
}
