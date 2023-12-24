using ModelShop.Models;
using System.ComponentModel.DataAnnotations;

namespace ModelShop.ViewModels
{
    public class ProfileViewModel
    {
        public Client Client { get; set; }

        public bool IsOwner { get; set; }

        public int Views { get; set; }
    }
}
