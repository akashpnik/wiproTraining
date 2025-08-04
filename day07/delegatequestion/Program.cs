// See https://aka.ms/new-console-template for more information
// create a delegate for a admin who is responsible for calculating the invoice(int tutionfess , int transportfees)
//and one more delegate which will print the invoice
using System;

class Program
{
    
    public delegate void CalculateInvoice(int tuitionFees, int transportFees);

    
    public delegate void PrintInvoice(int totalAmount);

    // Entry point
    static void Main()
    {
    
        CalculateInvoice calc = CalculateTotal;
        PrintInvoice print = DisplayInvoice;

        
        int tuition = 20000;
        int transport = 5000;

        
        int total = calc(tuition, transport);
        print(total);
    }

    
    static int CalculateTotal(int tuition, int transport)
    {
        return tuition + transport;
    }

    
    static void DisplayInvoice(int total)
    {
        Console.WriteLine($"Total Invoice Amount: {total}");
    }
}
