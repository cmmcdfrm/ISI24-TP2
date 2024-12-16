using api.Models;
using api.Repositories;

namespace api.Services
{
    public class ModelService : IModelService
    {
        private readonly IModelRepository _modelRepository;

        public ModelService(IModelRepository modelRepository)
        {
            _modelRepository = modelRepository;
        }

        public IEnumerable<Model> GetAllModels() => _modelRepository.GetAllModels();

        public void AddModel(Model model) => _modelRepository.AddModel(model);
    }
}
