using Microsoft.EntityFrameworkCore;
using ModelShop.Data.Contracts;
using ModelShop.Models;

namespace ModelShop.Data.Implementation
{
    public class ClientRepository : IClientRepository
    {
        private readonly ModelShopContext _context;
        public ClientRepository(ModelShopContext context)
        {
            _context = context;
        }

        public void Delete(Client entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Client entity)
        {
            throw new NotImplementedException();
        }

        public Client Get(int id)
        {
            return _context.Clients.Include(c => c.OwnedModels3D).FirstOrDefault(client => client.ClientID == id);
        }

        public IEnumerable<Client> GetAll()
        {
            return _context.Clients.ToList();
        }

        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            return await _context.Clients.ToListAsync();
        }

        public async Task<Client> GetAsync(int id)
        {
            return await _context.Clients.FirstOrDefaultAsync(client => client.ClientID == id);
        }

        public void Insert(Client entity)
        {
            _context.Clients.Add(entity);
        }

        public async Task InsertAsync(Client entity)
        {
            await _context.Clients.AddAsync(entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Client entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Client entity)
        {
            throw new NotImplementedException();
        }
    }
}
