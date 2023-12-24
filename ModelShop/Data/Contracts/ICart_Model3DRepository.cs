using ModelShop.Models;

namespace ModelShop.Data.Contracts
{
    public interface ICart_Model3DRepository: IRepository<Cart_Model3D>
    {
        public void DeleteById(int cartId, int modelId);
    }
}
