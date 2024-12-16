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

        public IEnumerable<Car> GetAllCars()
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
                    PlateNumber = reader.GetString(1),
                    ModelId = reader.GetInt32(2),
                    Price = reader.GetFloat(3)
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
                    PlateNumber = reader.GetString(1),
                    ModelId = reader.GetInt32(2),
                    Price = reader.GetFloat(3)
                };
            }
            throw new KeyNotFoundException("Car not found");
        }

        public void AddCar(Car car)
        {
            using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand("INSERT INTO Cars (PlateNumber, ModelId, Price) VALUES (@PlateNumber, @ModelId, @Price)", connection);
            command.Parameters.AddWithValue("@PlateNumber", car.PlateNumber);
            command.Parameters.AddWithValue("@ModelId", car.ModelId);
            command.Parameters.AddWithValue("@Price", car.Price);
            connection.Open();
            command.ExecuteNonQuery();
        }

        public void Update(Car car)
        {
            using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand("UPDATE Cars SET PlateNumber = @PlateNumber, ModelId = @ModelId, Price = @Price WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@PlateNumber", car.PlateNumber);
            command.Parameters.AddWithValue("@ModelId", car.ModelId);
            command.Parameters.AddWithValue("@Price", car.Price);
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
