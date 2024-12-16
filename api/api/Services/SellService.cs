using api.Models;
using api.Repositories;

namespace api.Services
{
    public class SellService : ISellService
    {
        private readonly ISellRepository _sellRepository;

        public SellService(ISellRepository sellRepository)
        {
            _sellRepository = sellRepository;
        }

        public IEnumerable<Sell> GetAllSells() => _sellRepository.GetAllSells();

        public void AddSell(Sell sell) => _sellRepository.AddSell(sell);
    }
}
