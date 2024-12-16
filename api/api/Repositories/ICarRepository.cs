using api.Models;

namespace api.Repositories
{
    public interface ICarRepository
    {
        IEnumerable<Car> GetAllCars();
        Car GetById(int id);
        void AddCar(Car car);
        void Update(Car car);
        void Delete(int id);
    }
}
