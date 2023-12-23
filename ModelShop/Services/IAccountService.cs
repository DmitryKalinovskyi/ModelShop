using ModelShop.Models;

namespace ModelShop.Services
{
    public interface IAccountService
    {
        bool Exist(string accountName);

        void Add(Client client);

        void Delete(string accountName);
    }
}
