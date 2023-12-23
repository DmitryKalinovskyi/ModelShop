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
            _context.Clients.Remove(entity);
        }

        public bool Exist(string username)
        {
            return _context.Clients.Any(c => c.UserName == username);
        }

        public async Task<bool> ExistAsync(string username)
        {
            return await _context.Clients.AnyAsync(c => c.UserName== username);
        }

        public Client GetById(string id)
        {
            return _context.Clients.Include(c => c.OwnedModels3D).FirstOrDefault(client => client.Id == id);
        }

        public IEnumerable<Client> GetAll()
        {
            return _context.Clients.ToList();
        }

        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            return await _context.Clients.ToListAsync();
        }

        public async Task<Client> GetByIdAsync(string id)
        {
            return await _context.Clients.FirstOrDefaultAsync(client => client.Id == id);
        }

        public Client GetByUsername(string username)
        {
            return _context.Clients.FirstOrDefault(c => c.UserName == username);
        }

        public async Task<Client> GetByUsernameAsync(string username)
        {
            return await _context.Clients.FirstOrDefaultAsync(c => c.UserName == username);
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
