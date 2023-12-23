using System.ComponentModel.DataAnnotations;

namespace ModelShop.Models
{
    public class Client
    {
        [Key]
        public int ClientID { get; set; }

        [MaxLength(50)]
        public string Firstname { get; set; }

        [MaxLength(50)]
        public string Lastname { get; set; }
        
        [MaxLength(50)]
        public string Username{ get; set; }

        public DateTime RegisterDate { get; set; }

        [MaxLength(60)]
        public string PasswordHash { get; set; }

        public bool IsAdmin { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        [MaxLength(200)]
        public string? AvatarImageSource { get; set; }

        // reference navigation

        public IEnumerable<Model3D> OwnedModels3D { get; set; }

        //public IEnumerable<ClientFollower> Followers { get; set; }  

        //public IEnumerable<ClientFollower> Following { get; set; }
    }
}
