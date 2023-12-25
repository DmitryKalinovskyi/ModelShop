using Microsoft.EntityFrameworkCore;
using ModelShop.Data.Contracts;
using ModelShop.Models;

namespace ModelShop.Data.Implementation
{
    public class ClientFollowerRepository : IClientFollowerRepository
    {
        private readonly ModelShopContext _context;
        public ClientFollowerRepository(ModelShopContext context) 
        {
            _context = context;
        }

        public void Delete(ClientFollower entity)
        {
            _context.Remove(entity);
        }

        public ClientFollower Get(string followerId, string followingId)
        {
            return _context.ClientFollowers
                .FirstOrDefault(cf => cf.FollowerID == followerId && cf.FollowingID == followingId);
        }

        public ICollection<ClientFollower> GetAll()
        {
            return _context.ClientFollowers.ToList();
        }

        public async Task<ICollection<ClientFollower>> GetAllAsync()
        {
            return await _context.ClientFollowers.ToListAsync();
        }

        public ICollection<ClientFollower> GetFollowers(string clientId)
        {
            return _context.ClientFollowers
                .Where(cf => cf.FollowingID == clientId)
                .Include(cf => cf.Follower)
                .ToList();
        }

        public ICollection<ClientFollower> GetFollowings(string clientId)
        {
            return _context.ClientFollowers
                .Where(cf => cf.FollowerID == clientId)
                .Include(cf => cf.Following)
                .ToList();
        }

        public void Insert(ClientFollower entity)
        {
            _context.ClientFollowers.Add(entity);
        }

        public async Task InsertAsync(ClientFollower entity)
        {
            await _context.ClientFollowers.AddAsync(entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(ClientFollower entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(ClientFollower entity)
        {
            throw new NotImplementedException();
        }
    }
}
