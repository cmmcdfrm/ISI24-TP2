using api.Models;
using api.Repositories;

namespace api.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public IEnumerable<Car> GetAllCars() => _carRepository.GetAllCars();

        public Car GetCarById(int id) => _carRepository.GetById(id);

        public void AddCar(Car car) => _carRepository.AddCar(car);

        public void UpdateCar(Car car) => _carRepository.Update(car);

        public void DeleteCar(int id) => _carRepository.Delete(id);
    }
}
