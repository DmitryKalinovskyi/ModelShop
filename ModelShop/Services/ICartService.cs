using ModelShop.Models;

namespace ModelShop.Services
{
    public interface ICartService
    {
        public Cart GetCart(string clientId);

        public void AddToCart(string clientId, int modelId);

        public void RemoveFromCart(string clientId, int modelId);

        public bool IsInCart(string clientId, int modelId);

        public bool IsOrdered(string clientId, int modelId);
    }
}
