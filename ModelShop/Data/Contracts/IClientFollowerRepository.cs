using ModelShop.Models;

namespace ModelShop.Data.Contracts
{
    public interface IClientFollowerRepository: IRepository<ClientFollower>
    {
        ClientFollower Get(string followerId, string followingId);

        ICollection<ClientFollower> GetFollowers(string clientId);
        
        ICollection<ClientFollower> GetFollowings(string clientId);
    }
}
