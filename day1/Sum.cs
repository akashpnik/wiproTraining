using System;


{
    internal class Sum
    {
        static void Main(string[] args)
        {
            // Declare variables
            int a;
            int b;

            // Input
            Console.WriteLine("Enter the first number: ");
            a = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter the second number: ");
            b = Convert.ToInt32(Console.ReadLine());

            // Output
            Console.WriteLine("Sum of the two numbers: " + (a + b));

            // Hold the console window
            Console.ReadKey();
        }
    }
}
