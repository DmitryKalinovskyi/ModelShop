using ModelShop.Models;

namespace ModelShop.Data.Contracts
{
    public interface IClientRepository: IRepository<Client>
    {
        Client GetById(string id);

        Task<Client> GetByIdAsync(string id);
    }
}
