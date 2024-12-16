using api.Models;
using Microsoft.Data.SqlClient;

namespace api.Repositories
{
    public class ModelRepository : IModelRepository
    {
        private readonly string _connectionString;

        public ModelRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Model> GetAllModels()
        {
            var models = new List<Model>();
            using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand("SELECT * FROM Models", connection);
            connection.Open();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                models.Add(new Model
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    BrandId = reader.GetInt32(2)
                });
            }
            return models;
        }

        public void AddModel(Model model)
        {
            using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand("INSERT INTO Models (Name, BrandId) VALUES (@Name, @BrandId)", connection);
            command.Parameters.AddWithValue("@Name", model.Name);
            command.Parameters.AddWithValue("@BrandId", model.BrandId);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}
