using api.Models;
using api.Repositories;

namespace api.Services
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;

        public BrandService(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public IEnumerable<Brand> GetAllBrands() => _brandRepository.GetAllBrands();

        public void AddBrand(Brand brand) => _brandRepository.AddBrand(brand);
    }
}
