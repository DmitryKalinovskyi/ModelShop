using ModelShop.Models;

namespace ModelShop.Data.Contracts
{
    public interface IClientRepository: IRepository<Client>
    {
        Client GetById(string id);

        Task<Client> GetByIdAsync(string id);

        int GetViewsById(string userId);

        Client GetByIdWithCart(string id);

        Task<Client> GetByIdWithCartAsync(string id);

        bool IsModel3DOrdered(string id, int modelId);

        ICollection<Model3D> GetOrderedModels(string clientId);
    }
}
