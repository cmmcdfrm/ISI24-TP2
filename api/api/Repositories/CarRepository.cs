using api.Models;
using Microsoft.Data.SqlClient;

namespace api.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly string _connectionString;

        public CarRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Car> GetAll()
        {
            var cars = new List<Car>();
            using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand("SELECT * FROM Cars", connection);
            connection.Open();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                cars.Add(new Car
                {
                    Id = reader.GetInt32(0),
                    Brand = reader.GetString(1),
                    Model = reader.GetString(2),
                    Year = reader.GetInt32(3),
                    Price = reader.GetDecimal(4)
                });
            }
            return cars;
        }

        public Car GetById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand("SELECT * FROM Cars WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Id", id);
            connection.Open();
            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new Car
                {
                    Id = reader.GetInt32(0),
                    Brand = reader.GetString(1),
                    Model = reader.GetString(2),
                    Year = reader.GetInt32(3),
                    Price = reader.GetDecimal(4)
                };
            }
            throw new KeyNotFoundException("Car not found");
        }

        public void Add(Car car)
        {
            using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand("INSERT INTO Cars (Brand, Model, Year, Price) VALUES (@Brand, @Model, @Year, @Price)", connection);
            command.Parameters.AddWithValue("@Brand", car.Brand);
            command.Parameters.AddWithValue("@Model", car.Model);
            command.Parameters.AddWithValue("@Year", car.Year);
            command.Parameters.AddWithValue("@Price", car.Price);
            connection.Open();
            command.ExecuteNonQuery();
        }

        public void Update(Car car)
        {
            using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand("UPDATE Cars SET Brand = @Brand, Model = @Model, Year = @Year, Price = @Price WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Brand", car.Brand);
            command.Parameters.AddWithValue("@Model", car.Model);
            command.Parameters.AddWithValue("@Year", car.Year);
            command.Parameters.AddWithValue("@Price", car.Price);
            command.Parameters.AddWithValue("@Id", car.Id);
            connection.Open();
            command.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand("DELETE FROM Cars WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Id", id);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}
