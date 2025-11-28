using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using InventoryManagementSystem.Data;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.DAL
{
    public class AuthDAL
    {
        private readonly DbConnection _db;
        public AuthDAL(DbConnection db)
        {
            _db = db;
        }

        private static string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

        public bool Register(string username, string password, string? fullName = null, string? email = null)
        {
            // check exists
            using var conn = _db.GetConnection();
            var check = new SqlCommand("SELECT COUNT(1) FROM Users WHERE Username=@u", conn);
            check.Parameters.AddWithValue("@u", username);
            conn.Open();
            var exists = Convert.ToInt32(check.ExecuteScalar() ?? 0) > 0;
            conn.Close();

            if (exists) return false;

            using var cmd = new SqlCommand("INSERT INTO Users (Username, PasswordHash, FullName, Email) VALUES (@u, @p, @f, @e)", conn);
            cmd.Parameters.AddWithValue("@u", username);
            cmd.Parameters.AddWithValue("@p", HashPassword(password));
            cmd.Parameters.AddWithValue("@f", (object?)fullName ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@e", (object?)email ?? DBNull.Value);

            conn.Open();
            cmd.ExecuteNonQuery();
            return true;
        }

        public User? Validate(string username, string password)
        {
            using var conn = _db.GetConnection();
            using var cmd = new SqlCommand("SELECT UserID, Username, PasswordHash, FullName, Email FROM Users WHERE Username=@u", conn);
            cmd.Parameters.AddWithValue("@u", username);
            conn.Open();
            using var rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                var storedHash = rdr["PasswordHash"].ToString() ?? "";
                if (storedHash == HashPassword(password))
                {
                    return new User
                    {
                        UserID = Convert.ToInt32(rdr["UserID"]),
                        Username = rdr["Username"].ToString() ?? "",
                        FullName = rdr["FullName"]?.ToString(),
                        Email = rdr["Email"]?.ToString()
                    };
                }
            }
            return null;
        }
    }
}
