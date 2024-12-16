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
    public class ModelsController : ControllerBase
    {
        private readonly IModelRepository _modelRepository;

        public ModelsController(IModelRepository modelRepository)
        {
            _modelRepository = modelRepository;
        }

        [HttpGet]
        public IActionResult GetAllModels()
        {
            var models = _modelRepository.GetAllModels();
            return Ok(models);
        }

        [HttpPost]
        public IActionResult AddModel([FromBody] Model model)
        {
            if (string.IsNullOrWhiteSpace(model.Name))
            {
                return BadRequest("Model name is required.");
            }

            _modelRepository.AddModel(model);
            return CreatedAtAction(nameof(GetAllModels), model);
        }
    }
}
