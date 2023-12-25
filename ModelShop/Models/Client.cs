using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelShop.Models
{
    public class Client: IdentityUser   
    {
        [MaxLength(50)]
        public string? Firstname { get; set; }

        [MaxLength(50)]
        public string? Lastname { get; set; }
        
        public DateTime RegisterDate { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }

        [MaxLength(200)]
        public string? AvatarImageSource { get; set; }


        // reference navigation

        public ICollection<Model3D> OwnedModels3D { get; set; }

        public ICollection<Order> Orders { get; set; }
        
        public Cart? Cart { get; set; }

        [InverseProperty("Follower")]
        public ICollection<ClientFollower>? Followers { get; set; }

        [InverseProperty("Following")]
        public ICollection<ClientFollower>? Followings { get; set; }
    }
}
