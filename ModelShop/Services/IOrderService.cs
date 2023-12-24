using ModelShop.Models;

namespace ModelShop.Services
{
    public interface IOrderService
    {
        Order CompleteOrder(string clientId);

        IEnumerable<Model3D> GetOrderedModels3D(string clientId);
    }
}
