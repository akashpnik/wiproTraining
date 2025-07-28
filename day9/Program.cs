// File: Program.cs
using System;
using CalculatorLib;

namespace CalculatorApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Calculator calc = new Calculator();

            Console.WriteLine("=== Calculator Demo ===");
            Console.WriteLine($"Add(3, 3): {calc.Add(3, 3)}");
            Console.WriteLine($"Subtract(10, 4): {calc.Subtract(10, 4)}");
            Console.WriteLine($"Multiply(2, 3): {calc.Multiply(2, 3)}");
            Console.WriteLine($"Divide(10, 2): {calc.Divide(10, 2)}");

            try
            {
                Console.WriteLine($"Divide(10, 0): {calc.Divide(10, 0)}");
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
