using Microsoft.EntityFrameworkCore;
using ModelShop.Data.Contracts;
using ModelShop.Models;

namespace ModelShop.Data.Implementation
{
    public class CartRepository : ICartRepository
    {
        private readonly ModelShopContext _context;

        public CartRepository(ModelShopContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Delete(Cart entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.Carts.Remove(entity);
        }

        public ICollection<Cart> GetAll()
        {
            return _context.Carts.ToList();
        }

        public async Task<ICollection<Cart>> GetAllAsync()
        {
            return await _context.Carts.ToListAsync();
        }

        public Cart GetCartByClientId(string clientId)
        {
            return _context.Carts
                .Include(c => c.Cart_Models3D)
                .ThenInclude(cart_model3d => cart_model3d.Model3D)
                .ThenInclude(model3D => model3D.Owner)
                .FirstOrDefault(cart => cart.ClientID == clientId);
        }

        public async Task<Cart> GetCartByClientIdAsync(string clientId)
        {
            return await _context.Carts
                .Include(c => c.Cart_Models3D)
                .ThenInclude(cart_model3d => cart_model3d.Model3D)
                .ThenInclude(model3D => model3D.Owner)
                .FirstOrDefaultAsync(cart => cart.ClientID == clientId);
        }

        public void Insert(Cart entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.Carts.Add(entity);
        }

        public async Task InsertAsync(Cart entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await _context.Carts.AddAsync(entity);
        }

        public bool IsInCart(string clientId, int modelId)
        {
            return _context.Carts
                .Include(c => c.Cart_Models3D)
                .FirstOrDefault(c => c.ClientID == clientId)
                .Cart_Models3D
                .Any(cart_model3d => cart_model3d.Model3DID == modelId);
        }

        public async Task<bool> IsInCartAsync(string clientId, int modelId)
        {
            var cart = await _context.Carts
                .Include(c => c.Cart_Models3D)
                .FirstOrDefaultAsync(c => c.ClientID == clientId);

            return cart
                .Cart_Models3D
                .Any(cart_model3d => cart_model3d.Model3DID == modelId);
        }
            
        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(Cart entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task UpdateAsync(Cart entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
