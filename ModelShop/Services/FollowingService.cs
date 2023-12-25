using ModelShop.Data.Contracts;
using ModelShop.Models;

namespace ModelShop.Services
{
    public class FollowingService : IFollowingService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IClientFollowerRepository _clientFollowerRepository;
        public FollowingService(IClientRepository clientRepository,
            IClientFollowerRepository clientFollowerRepository)
        {
            _clientRepository = clientRepository;
            _clientFollowerRepository = clientFollowerRepository;
        }


        public void Follow(string clientId, string followerId)
        {
            if(IsFollower(clientId, followerId))
            {
                throw new InvalidOperationException("You already followed");
            }

            _clientFollowerRepository.Insert(new ClientFollower
            {
                FollowerID = followerId,
                FollowingID = clientId
            });

            _clientFollowerRepository.Save();
        }

        public ICollection<Client> GetFollowers(string clientId)
        {
            return _clientFollowerRepository.GetFollowers(clientId).Select(cf => cf.Follower).ToList();
        }

        public ICollection<Client> GetFollowings(string clientId)
        {
            return _clientFollowerRepository.GetFollowings(clientId).Select(cf => cf.Following).ToList();
        }

        public bool IsFollower(string clientId, string followerId)
        {
            return _clientFollowerRepository.Get(followerId, clientId) != null;
        }

        public void Unfollow(string clientId, string followerId)
        {
            if(IsFollower(clientId, followerId) == false)
            {
                throw new InvalidOperationException("You are not followed");
            }

            _clientFollowerRepository.Delete(_clientFollowerRepository.Get(followerId, clientId));

            _clientFollowerRepository.Save();
        }

    }
}
