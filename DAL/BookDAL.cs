using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using DTO;

namespace DAL
{
    public class BookDAL
    {
        //private readonly string connectionString = @"Data Source=DELLVOSTRO;Initial Catalog=LibraryDB;Integrated Security=True"; // Thay bằng chuỗi kết nối của bạn

        private readonly string connectionString = @"Data Source=DELLVOSTRO;Initial Catalog=LibraryDB;Integrated Security=True;Trust Server Certificate=True"; // Thay bằng chuỗi kết nối của bạn

        public void AddBook(BookDTO book)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Books (Title, Author, Category) VALUES (@Title, @Author, @Category)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Title", book.Title);
                command.Parameters.AddWithValue("@Author", book.Author);
                command.Parameters.AddWithValue("@Category", book.Category);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public void UpdateBook(BookDTO book)
        {
            string query = "UPDATE Books SET Title = @Title, Author = @Author, Category = @Category WHERE BookId = @BookId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@BookId", book.BookId);
                cmd.Parameters.AddWithValue("@Title", book.Title);
                cmd.Parameters.AddWithValue("@Author", book.Author);
                cmd.Parameters.AddWithValue("@Category", book.Category);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        public void DeleteBook(int bookId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Books WHERE BookId = @BookId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@BookId", bookId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public List<BookDTO> GetAllBooks()
        {
            List<BookDTO> books = new List<BookDTO>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Books";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    books.Add(new BookDTO
                    {
                        BookId = reader.GetInt32(0),
                        Title = reader.GetString(1),
                        Author = reader.GetString(2),
                        Category = reader.GetString(3)
                    });
                }
            }
            return books;
        }

        //
        public List<BookDTO> GetBooks()
        {
            List<BookDTO> books = new List<BookDTO>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT BookId, Title, Author, Category FROM Books";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        books.Add(new BookDTO
                        {
                            BookId = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Author = reader.IsDBNull(2) ? null : reader.GetString(2),
                            Category = reader.IsDBNull(3) ? null : reader.GetString(3)
                        });
                    }
                }
            }

            return books;
        }
        public BookDTO GetBookById(int bookId)
        {
            BookDTO book = null;
            string query = "SELECT * FROM Books WHERE BookId = @BookId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@BookId", bookId);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    book = new BookDTO
                    {
                        BookId = Convert.ToInt32(reader["BookId"]),
                        Title = reader["Title"].ToString(),
                        Author = reader["Author"].ToString(),
                        Category = reader["Category"].ToString()
                    };
                }

                conn.Close();
            }

            return book;
        }
    }
}
