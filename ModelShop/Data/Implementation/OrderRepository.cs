using Microsoft.EntityFrameworkCore;
using ModelShop.Data.Contracts;
using ModelShop.Models;

namespace ModelShop.Data.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ModelShopContext _context;
        public OrderRepository(ModelShopContext context)
        {
            _context = context;
        }

        public void Delete(Order entity)
        {
            _context.Remove(entity);
        }

        public ICollection<Order> GetAll()
        {
            return _context.Orders
                .Include(o => o.Client)
				.Include(o => o.OrderItems)
				.ThenInclude(oi => oi.Model3D)
				.ToList();
        }

        public async Task<ICollection<Order>> GetAllAsync()
        {
            return await _context.Orders
                .Include(o => o.Client)
				.Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Model3D)
				.ToListAsync();
        }

        public void Insert(Order entity)
        {
            _context.Orders.Add(entity);
        }

        public async Task InsertAsync(Order entity)
        {
            await _context.Orders.AddAsync(entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Order entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Order entity)
        {
            throw new NotImplementedException();
        }
    }
}
