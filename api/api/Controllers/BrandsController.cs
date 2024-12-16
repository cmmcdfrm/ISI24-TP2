using api.Models;
using api.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    [Produces(MediaTypeNames.Application.Json)]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandRepository _brandRepository;

        public BrandsController(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        [HttpGet]
        public IActionResult GetAllBrands()
        {
            var brands = _brandRepository.GetAllBrands();
            return Ok(brands);
        }

        [HttpPost]
        public IActionResult AddBrand([FromBody] Brand brand)
        {
            if (string.IsNullOrWhiteSpace(brand.Name))
            {
                return BadRequest("Brand name is required.");
            }

            _brandRepository.AddBrand(brand);
            return CreatedAtAction(nameof(GetAllBrands), brand);
        }
    }
}
