using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using ModelShop.Data.Contracts;
using ModelShop.Models;
using System.Security.Permissions;

namespace ModelShop.Data.Implementation
{
    public class Model3DRepository : IModel3DRepository
    {
        private readonly ModelShopContext _context;
        public Model3DRepository(ModelShopContext context) 
        {
            _context = context;
        }

        public void Delete(Model3D entity)
        {
            _context.Models3D.Remove(entity);
        }

        public Model3D Get(int id)
        {
            return _context.Models3D
                .Include(m => m.Owner)
                .Include(m => m.ModelCategory)
                .FirstOrDefault(model => model.Model3DID == id);
        }

        public ICollection<Model3D> GetAll()
        {
            return _context.Models3D
                .Include(m => m.Owner)
                .Include(m => m.ModelCategory)
                .ToList();
        }

        public async Task<ICollection<Model3D>> GetAllAsync()
        {
            return await _context.Models3D
                .Include(m => m.Owner)
                .Include(m => m.ModelCategory)
                .ToListAsync();
        }

        public async Task<Model3D> GetAsync(int id)
        {
            return await _context.Models3D
                .Include(m => m.Owner)
                .Include(m => m.ModelCategory)
                .FirstOrDefaultAsync(model => model.Model3DID == id);
        }

        public void Insert(Model3D entity)
        {
            _context.Models3D.Add(entity);
        }

        public async Task InsertAsync(Model3D entity)
        {
            await _context.Models3D.AddAsync(entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task<IEnumerable<Model3D>> SearchAsync(string? search, int modelCategoryID = -1, decimal minPrice = 0, decimal maxPrice = 1000000)
        {
            search = search == null ? "" : search;
            search = search.ToLower();
            return await _context.Models3D
                .Where(m => m.Title.ToLower().Contains(search) && m.Price >= minPrice && m.Price <= maxPrice && 
                (modelCategoryID == -1 || m.ModelCategoryID == modelCategoryID))
                .Include(m => m.Owner)
                .ToListAsync();
        }

        public void Update(Model3D entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Model3D entity)
        {
            throw new NotImplementedException();
        }
    }
}
