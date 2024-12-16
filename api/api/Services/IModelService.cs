using api.Models;

namespace api.Services
{
    public interface IModelService
    {
        IEnumerable<Model> GetAllModels();
        void AddModel(Model model);
    }
}
