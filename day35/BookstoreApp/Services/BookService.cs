using System.Data;
using System.Data.SqlClient;
using BookstoreApp.Models;

namespace BookstoreApp.Services
{
    public class BookService
    {
        private readonly string _connectionString;

        public BookService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Book> GetAllBooks()
        {
            var books = new List<Book>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM Books ORDER BY Title", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        books.Add(new Book
                        {
                            Id = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Author = reader.GetString(2),
                            ISBN = reader.GetString(3),
                            Price = reader.GetDecimal(4),
                            PublishedDate = reader.IsDBNull(5) ? null : reader.GetDateTime(5)
                        });
                    }
                }
            }

            return books;
        }

        public Book GetBookById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM Books WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Book
                            {
                                Id = reader.GetInt32(0),
                                Title = reader.GetString(1),
                                Author = reader.GetString(2),
                                ISBN = reader.GetString(3),
                                Price = reader.GetDecimal(4),
                                PublishedDate = reader.IsDBNull(5) ? null : reader.GetDateTime(5)
                            };
                        }
                    }
                }
            }

            return null;
        }

        public int AddBookUsingStoredProcedure(Book book)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("sp_AddBook", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    
                    command.Parameters.AddWithValue("@Title", book.Title);
                    command.Parameters.AddWithValue("@Author", book.Author);
                    command.Parameters.AddWithValue("@ISBN", book.ISBN);
                    command.Parameters.AddWithValue("@Price", book.Price);
                    command.Parameters.AddWithValue("@PublishedDate", 
                        book.PublishedDate.HasValue ? (object)book.PublishedDate.Value : DBNull.Value);
                    
                    var result = command.ExecuteScalar();
                    return Convert.ToInt32(result);
                }
            }
        }

        public void UpdateBookUsingStoredProcedure(Book book)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("sp_UpdateBook", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    
                    command.Parameters.AddWithValue("@Id", book.Id);
                    command.Parameters.AddWithValue("@Title", book.Title);
                    command.Parameters.AddWithValue("@Author", book.Author);
                    command.Parameters.AddWithValue("@ISBN", book.ISBN);
                    command.Parameters.AddWithValue("@Price", book.Price);
                    command.Parameters.AddWithValue("@PublishedDate", 
                        book.PublishedDate.HasValue ? (object)book.PublishedDate.Value : DBNull.Value);
                    
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteBook(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("sp_DeleteBook", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public DataSet GetAllBooksDataSet()
        {
            var dataSet = new DataSet();

            using (var connection = new SqlConnection(_connectionString))
            using (var adapter = new SqlDataAdapter("SELECT * FROM Books ORDER BY Title", connection))
            {
                adapter.Fill(dataSet, "Books");
            }

            return dataSet;
        }

        public void UpdateBookUsingDataSet(DataSet dataSet)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var adapter = new SqlDataAdapter("SELECT * FROM Books", connection))
            {
                var commandBuilder = new SqlCommandBuilder(adapter);
                adapter.Update(dataSet, "Books");
            }
        }

        public List<Book> GetBooksUsingDataReader()
        {
            var books = new List<Book>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM Books", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        books.Add(new Book
                        {
                            Id = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Author = reader.GetString(2),
                            ISBN = reader.GetString(3),
                            Price = reader.GetDecimal(4),
                            PublishedDate = reader.IsDBNull(5) ? null : reader.GetDateTime(5)
                        });
                    }
                }
            }

            return books;
        }

        public DataSet GetBooksUsingDataAdapter()
        {
            var dataSet = new DataSet();

            using (var connection = new SqlConnection(_connectionString))
            using (var adapter = new SqlDataAdapter("SELECT * FROM Books", connection))
            {
                adapter.Fill(dataSet, "Books");
            }

            return dataSet;
        }
    }
}