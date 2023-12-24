using ModelShop.Models;

namespace ModelShop.Data.Contracts
{
    public interface IOrder_Model3DRepository: IRepository<Order_Model3D>
    {
        Order_Model3D Get(int orderId, int modelId);

        Task<Order_Model3D> GetAsync(int orderId, int modelId);
    }
}
