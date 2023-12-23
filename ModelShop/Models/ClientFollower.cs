using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ModelShop.Models
{
    public class ClientFollower
    {
        [Key]
        [Column(Order = 1)]
        public int FollowerId { get; set; }

        [Key]
        [Column(Order = 2)]
        public int FollowingId { get; set; }

        //[ForeignKey("FollowerId")]
        //public Client Follower { get; set; }

        //[ForeignKey("FollowingId")]
        //public Client Following { get; set; }
    }
}
