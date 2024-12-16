using api.Models;

namespace api.Repositories
{
    public interface ISellRepository
    {
        IEnumerable<Sell> GetAllSells();
        void AddSell(Sell sell);
    }
}
