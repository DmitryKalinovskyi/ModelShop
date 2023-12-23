using ModelShop.Models;

namespace ModelShop.Data.Contracts
{
    public interface IModelCategoryRepository: IRepository<ModelCategory>
    {
        ModelCategory Get(int id);

        Task<ModelCategory> GetAsync(int id);
    }
}
