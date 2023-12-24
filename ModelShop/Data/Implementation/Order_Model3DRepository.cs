using Microsoft.EntityFrameworkCore;
using ModelShop.Data.Contracts;
using ModelShop.Models;

namespace ModelShop.Data.Implementation
{
    public class Order_Model3DRepository : IOrder_Model3DRepository
    {
        private readonly ModelShopContext _context;

        public Order_Model3DRepository(ModelShopContext context)
        {
            _context = context;
        }

        public void Delete(Order_Model3D entity)
        {
            _context.Client_Models3D.Remove(entity);
        }

        public Order_Model3D Get(int orderId, int modelId)
        {
            return _context.Client_Models3D
                .FirstOrDefault(o_m => o_m.OrderID== orderId && o_m.Model3DID == modelId);
        }

        public ICollection<Order_Model3D> GetAll()
        {
            return _context.Client_Models3D.ToList();
        }


        public async Task<ICollection<Order_Model3D>> GetAllAsync()
        {
            return await _context.Client_Models3D.ToListAsync();
        }

        public Task<Order_Model3D> GetAsync(int orderId, int modelId)
        {
            return _context.Client_Models3D
                 .FirstOrDefaultAsync(o_m => o_m.OrderID == orderId && o_m.Model3DID == modelId);
        }

        public void Insert(Order_Model3D entity)
        {
            _context.Client_Models3D.Add(entity);
        }

        public async Task InsertAsync(Order_Model3D entity)
        {
            await _context.Client_Models3D.AddAsync(entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Order_Model3D entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Order_Model3D entity)
        {
            throw new NotImplementedException();
        }
    }
}
