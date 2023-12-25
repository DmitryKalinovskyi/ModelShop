using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ModelShop.Models
{

    public class ClientFollower
    {
        public int ClientFollowerId { get; set; } 

        [ForeignKey("Follower")]
        public string? FollowerID { get; set; }

        [ForeignKey("Following")]
        public string? FollowingID { get; set; }

        public Client? Follower { get; set; }

        public Client? Following { get; set; }
    }
}
