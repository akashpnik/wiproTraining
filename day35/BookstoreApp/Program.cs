// See https://aka.ms/new-console-template for more information
using System;
using System.Data;
using BookstoreApp.Models;
using BookstoreApp.Services;

namespace BookstoreApp
{
    class Program
    {
        static void Main(string[] args)
        {
            const string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=BookstoreAdoDB;Integrated Security=true;";

            var bookService = new BookService(connectionString);

            try
            {
                Console.WriteLine("Bookstore Application Demo\n");

                // Add a book
                var newBook = new Book
                {
                    Title = "C# Programming",
                    Author = "John Doe",
                    ISBN = "978-1234567890",
                    Price = 49.99m,
                    PublishedDate = DateTime.Now
                };

                int newId = bookService.AddBookUsingStoredProcedure(newBook);
                Console.WriteLine($"✓ Added book with ID: {newId}");

                // Add another book
                var secondBook = new Book
                {
                    Title = "ADO.NET Essentials",
                    Author = "Jane Smith",
                    ISBN = "978-0987654321",
                    Price = 39.99m,
                    PublishedDate = DateTime.Now.AddMonths(-6)
                };

                int secondId = bookService.AddBookUsingStoredProcedure(secondBook);
                Console.WriteLine($"✓ Added book with ID: {secondId}");

                // Display all books using DataReader
                Console.WriteLine("\n📚 All Books (using DataReader):");
                var books = bookService.GetAllBooks();
                foreach (var book in books)
                {
                    Console.WriteLine($"#{book.Id}: {book.Title} by {book.Author} - ${book.Price}");
                }

                // Display using DataSet
                Console.WriteLine("\n📊 Books (using DataSet):");
                DataSet bookDataSet = bookService.GetAllBooksDataSet();
                DataTable bookTable = bookDataSet.Tables["Books"];

                foreach (DataRow row in bookTable.Rows)
                {
                    Console.WriteLine($"#{row["Id"]}: {row["Title"]} - ${row["Price"]}");
                }

                // Update a book
                if (books.Count > 0)
                {
                    var bookToUpdate = books[0];
                    bookToUpdate.Price = 59.99m;
                    bookService.UpdateBookUsingStoredProcedure(bookToUpdate);
                    Console.WriteLine($"\n✓ Updated book #{bookToUpdate.Id} price to ${bookToUpdate.Price}");
                }

                // Delete a book
                if (books.Count > 1)
                {
                    bookService.DeleteBook(books[1].Id);
                    Console.WriteLine($"✓ Deleted book with ID: {books[1].Id}");
                }

                // Show final list
                Console.WriteLine("\n🎯 Final Book List:");
                var finalBooks = bookService.GetAllBooks();
                foreach (var book in finalBooks)
                {
                    Console.WriteLine($"#{book.Id}: {book.Title} - ${book.Price}");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}