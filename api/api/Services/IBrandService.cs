using api.Models;

namespace api.Services
{
    public interface IBrandService
    {
        IEnumerable<Brand> GetAllBrands();
        void AddBrand(Brand brand);
    }
}
