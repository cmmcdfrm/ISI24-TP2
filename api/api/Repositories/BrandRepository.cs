using api.Models;
using Microsoft.Data.SqlClient;

namespace api.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly string _connectionString;

        public BrandRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Brand> GetAllBrands()
        {
            var brands = new List<Brand>();
            using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand("SELECT * FROM Brands", connection);
            connection.Open();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                brands.Add(new Brand { Id = reader.GetInt32(0), Name = reader.GetString(1) });
            }
            return brands;
        }

        public void AddBrand(Brand brand)
        {
            using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand("INSERT INTO Brands (Name) VALUES (@Name)", connection);
            command.Parameters.AddWithValue("@Name", brand.Name);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}
