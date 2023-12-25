using ModelShop.Models;

namespace ModelShop.Services
{
    public interface IFollowingService
    {
        public bool IsFollower(string clientId, string followerId);

        public ICollection<Client> GetFollowers(string clientId);

        public ICollection<Client> GetFollowings(string clientId);

        public void Follow(string clientId, string followerId);

        public void Unfollow(string clientId, string followerId);
    }
}
