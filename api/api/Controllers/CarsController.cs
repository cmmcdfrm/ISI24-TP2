using api.Models;
using api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [Produces(MediaTypeNames.Application.Json)]
    public class CarsController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarsController(ICarService carService)
        {
            _carService = carService;
        }

        /// <summary>
        /// Obter todos os carros disponíveis.
        /// </summary>
        /// <returns>Lista de carros.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAllCars()
        {
            var cars = _carService.GetAllCars();
            return Ok(cars);
        }

        /// <summary>
        /// Obter informações de um carro pelo ID.
        /// </summary>
        /// <param name="id">ID do carro.</param>
        /// <returns>Dados do carro.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            try
            {
                var car = _carService.GetCarById(id);
                return Ok(car);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Car not found");
            }
        }

        /// <summary>
        /// Adicionar um novo carro ao stand.
        /// </summary>
        /// <param name="car">Dados do carro a ser adicionado.</param>
        /// <returns>Carro criado.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddCar([FromBody] Car car)
        {
            _carService.AddCar(car);
            return CreatedAtAction(nameof(GetById), new { id = car.Id }, car);
        }

        /// <summary>
        /// Atualizar as informações de um carro existente.
        /// </summary>
        /// <param name="id">ID do carro.</param>
        /// <param name="car">Novos dados do carro.</param>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update(int id, [FromBody] Car car)
        {
            car.Id = id;
            _carService.UpdateCar(car);
            return NoContent();
        }

        /// <summary>
        /// Remover um carro do stand.
        /// </summary>
        /// <param name="id">ID do carro.</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            _carService.DeleteCar(id);
            return NoContent();
        }
    }
}
