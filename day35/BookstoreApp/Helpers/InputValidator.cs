using BookstoreApp.Models;

namespace BookstoreApp.Helpers
{
    public static class InputValidator
    {
        public static bool IsValidBook(Book book)
        {
            if (string.IsNullOrWhiteSpace(book.Title) || book.Title.Length > 200)
                return false;

            if (string.IsNullOrWhiteSpace(book.Author) || book.Author.Length > 100)
                return false;

            if (string.IsNullOrWhiteSpace(book.ISBN) || book.ISBN.Length > 20)
                return false;

            if (book.Price <= 0)
                return false;

            return true;
        }

        public static string SanitizeInput(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            return input.Replace("'", "''")
                       .Replace(";", "")
                       .Replace("--", "")
                       .Replace("/*", "")
                       .Replace("*/", "");
        }
    }
}