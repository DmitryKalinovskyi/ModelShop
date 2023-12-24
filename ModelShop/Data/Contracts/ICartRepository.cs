using ModelShop.Models;

namespace ModelShop.Data.Contracts
{
    public interface ICartRepository: IRepository<Cart>
    {
        Cart GetCartByClientId(string clientId);

        Task<Cart> GetCartByClientIdAsync(string clientId);

        bool IsInCart(string clientId, int modelId);

        Task<bool> IsInCartAsync(string clientId, int modelId);
    }
}
