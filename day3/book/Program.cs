using System;

using Booky;
class Program
{
    public static void Main(string[] args)
    {
        // Prompt and input
        Console.WriteLine("Enter book's title:");
        string title = Console.ReadLine();

        Console.WriteLine("Enter book's author:");
        string author = Console.ReadLine();

        Console.WriteLine("Enter book's year:");
        int year = Convert.ToInt32(Console.ReadLine());

        // Create book instance
        Book myBook = new Book(title, author, year);

        // Output
        Console.WriteLine("Book Title: " + myBook.Title);
        Console.WriteLine("Book Author: " + myBook.Author);
        Console.WriteLine("Book Year: " + myBook.Year);
    }
}