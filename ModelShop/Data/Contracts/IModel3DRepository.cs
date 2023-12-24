using ModelShop.Models;

namespace ModelShop.Data.Contracts
{
    public interface IModel3DRepository: IRepository<Model3D>
    {
        Model3D Get(int id);

        Task<Model3D> GetAsync(int id);

        Task<IEnumerable<Model3D>> SearchAsync(string search);
    }
}
