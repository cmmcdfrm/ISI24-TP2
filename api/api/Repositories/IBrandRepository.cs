using api.Models;

namespace api.Repositories
{
    public interface IBrandRepository
    {
        IEnumerable<Brand> GetAllBrands();
        void AddBrand(Brand brand);
    }
}
