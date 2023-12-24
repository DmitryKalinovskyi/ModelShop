using Microsoft.EntityFrameworkCore;
using ModelShop.Data.Contracts;
using ModelShop.Models;

namespace ModelShop.Data.Implementation
{
    public class ModelCategoryRepository: IModelCategoryRepository
    {
        private readonly ModelShopContext _context;

        public ModelCategoryRepository(ModelShopContext context)
        {
            _context = context;
        }

        public void Delete(ModelCategory entity)
        {
            _context.ModelCategories.Remove(entity);
        }

        public ModelCategory Get(int id)
        {
            return _context.ModelCategories.FirstOrDefault(modelCategory => modelCategory.ModelCategoryID == id);
        }

        public ICollection<ModelCategory> GetAll()
        {
            return _context.ModelCategories.ToList();
        }

        public async Task<ICollection<ModelCategory>> GetAllAsync()
        {
            return await _context.ModelCategories.ToListAsync();
        }

        public async Task<ModelCategory> GetAsync(int id)
        {
            return await _context.ModelCategories.FirstOrDefaultAsync(modelCategory => modelCategory.ModelCategoryID == id);
        }

        public void Insert(ModelCategory entity)
        {
            _context.ModelCategories.Add(entity);
        }

        public async Task InsertAsync(ModelCategory entity)
        {
            await _context.ModelCategories.AddAsync(entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(ModelCategory entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(ModelCategory entity)
        {
            throw new NotImplementedException();
        }
    }
}
