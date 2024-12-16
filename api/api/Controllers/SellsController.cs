using api.Models;
using api.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [Produces(MediaTypeNames.Application.Json)]
    public class SellsController : ControllerBase
    {
        private readonly ISellRepository _sellRepository;

        public SellsController(ISellRepository sellRepository)
        {
            _sellRepository = sellRepository;
        }

        [HttpGet]
        public IActionResult GetAllSells()
        {
            var sells = _sellRepository.GetAllSells();
            return Ok(sells);
        }

        [HttpPost]
        public IActionResult AddSell([FromBody] Sell sell)
        {
            if (sell.UserId <= 0 || sell.CarId <= 0)
            {
                return BadRequest("Valid UserId and CarId are required.");
            }

            _sellRepository.AddSell(sell);
            return CreatedAtAction(nameof(GetAllSells), sell);
        }
    }
}
