﻿using api.Models;
using Microsoft.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddUser(User user)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            // Hash the password
            user.Password = HashPassword(user.Password);

            var command = new SqlCommand(
                "INSERT INTO Users (Username, Password) VALUES (@Username, @Password)", connection);

            command.Parameters.AddWithValue("@Username", user.Username);
            command.Parameters.AddWithValue("@Password", user.Password);

            command.ExecuteNonQuery();
        }

        public User? GetByUsername(string username)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var command = new SqlCommand("SELECT * FROM Users WHERE Username = @Username", connection);
            command.Parameters.AddWithValue("@Username", username);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new User
                {
                    Id = reader.GetInt32(0),
                    Username = reader.GetString(1),
                    Password = reader.GetString(2)
                };
            }

            return null;
        }

        public static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }
}
