using api.Models;
using Microsoft.Data.SqlClient;

namespace api.Repositories
{
    public class SellRepository : ISellRepository
    {
        private readonly string _connectionString;

        public SellRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Sell> GetAllSells()
        {
            var sells = new List<Sell>();
            using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand("SELECT * FROM Sells", connection);
            connection.Open();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                sells.Add(new Sell
                {
                    Id = reader.GetInt32(0),
                    UserId = reader.GetInt32(1),
                    CarId = reader.GetInt32(2)
                });
            }
            return sells;
        }

        public void AddSell(Sell sell)
        {
            using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand("INSERT INTO Sells (UserId, CarId) VALUES (@UserId, @CarId)", connection);
            command.Parameters.AddWithValue("@UserId", sell.UserId);
            command.Parameters.AddWithValue("@CarId", sell.CarId);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}
