using api.Models;

namespace api.Services
{
    public interface ISellService
    {
        IEnumerable<Sell> GetAllSells();
        void AddSell(Sell sell);
    }
}
