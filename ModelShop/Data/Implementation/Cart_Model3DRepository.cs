using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ModelShop.Data.Contracts;
using ModelShop.Models;

namespace ModelShop.Data.Implementation
{
    public class Cart_Model3DRepository : ICart_Model3DRepository
    {
        private readonly ModelShopContext _context;
        public Cart_Model3DRepository(ModelShopContext context)
        {
            _context = context;
        }
        public void Delete(Cart_Model3D entity)
        {
            _context.Remove(entity);
        }

        public ICollection<Cart_Model3D> GetAll()
        {
            return _context.Cart_Models3D.ToList();
        }

        public async Task<ICollection<Cart_Model3D>> GetAllAsync()
        {
            return await _context.Cart_Models3D.ToListAsync();
        }

        public void Insert(Cart_Model3D entity)
        {
            _context.Add(entity);
        }

        public async Task InsertAsync(Cart_Model3D entity)
        {
            await _context.AddAsync(entity);
        }

        public void DeleteById(int cartId, int modelId)
        {
            _context.RemoveRange(
                _context.Cart_Models3D.Where(c => c.CartID == cartId && c.Model3DID == modelId));
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Cart_Model3D entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Cart_Model3D entity)
        {
            throw new NotImplementedException();
        }
    }
}
