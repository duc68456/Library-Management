using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using Microsoft.Data.SqlClient;

namespace DAL
{
    public class ReaderDAL
    {
        private string connectionString = @"Data Source=DELLVOSTRO;Initial Catalog=LibraryDB;Integrated Security=True;Trust Server Certificate=True";

        public List<ReaderDTO> GetAllReaders()
        {
            List<ReaderDTO> readers = new List<ReaderDTO>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Readers", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    readers.Add(new ReaderDTO
                    {
                        ReaderId = Convert.ToInt32(reader["ReaderId"]),
                        Name = reader["Name"].ToString(),
                        Gender = reader["Gender"].ToString(),
                        IDNumber = reader["IDNumber"].ToString()
                    });
                }
            }
            return readers;
        }

        public bool AddReader(ReaderDTO reader)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Readers (Name, Gender, IDNumber) VALUES (@Name, @Gender, @IDNumber)", conn);
                cmd.Parameters.AddWithValue("@Name", reader.Name);
                cmd.Parameters.AddWithValue("@Gender", reader.Gender);
                cmd.Parameters.AddWithValue("@IDNumber", reader.IDNumber);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public void UpdateReader(ReaderDTO reader)
        {
            string query = "UPDATE Readers SET Name = @Name, Gender = @Gender, IDNumber = @IDNumber WHERE ReaderId = @ReaderId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ReaderId", reader.ReaderId);
                cmd.Parameters.AddWithValue("@Name", reader.Name);
                cmd.Parameters.AddWithValue("@Gender", reader.Gender);
                cmd.Parameters.AddWithValue("@IDNumber", reader.IDNumber);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void DeleteReader(int readerId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Readers WHERE ReaderId = @ReaderId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ReaderId", readerId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public ReaderDTO GetReaderById(int readerId)
        {
            string query = "SELECT * FROM Readers WHERE ReaderId = @ReaderId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ReaderId", readerId);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new ReaderDTO
                    {
                        ReaderId = Convert.ToInt32(reader["ReaderId"]),
                        Name = reader["Name"].ToString(),
                        Gender = reader["Gender"].ToString(),
                        IDNumber = reader["IDNumber"].ToString()
                    };
                }

                return null; // Không tìm thấy độc giả
            }
        }
    }
}
