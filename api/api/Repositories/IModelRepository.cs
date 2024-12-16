using api.Models;

namespace api.Repositories
{
    public interface IModelRepository
    {
        IEnumerable<Model> GetAllModels();
        void AddModel(Model model);
    }
}
