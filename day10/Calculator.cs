using System;
namespace CalculatorLib { 
public interface ICalculator
{
    int Add(int a, int b);

    int Subtract(int a, int b);

    int Multiply(int a, int b);

    int Divide(int a, int b);
    //{
    //    if (b == 0)
    //        throw new DivideByZeroException("Cannot divide by zero.");
    //    return a / b;
    //}
}
public class Calculator : ICalculator
{
    public int Add(int a, int b)
    {
        return a + b;
    }
    public int Subtract(int a, int b)
    {
        return a - b;
    }
    public int Multiply(int a, int b)
    {
        return a * b;
    }
    public int Divide(int a, int b)
    {
        if (b == 0)
            throw new DivideByZeroException("Cannot divide by zero.");
        return a / b;
    }

}
}