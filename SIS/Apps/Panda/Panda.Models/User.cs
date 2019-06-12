using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Panda.Models
{
    public class User
    {
        public User()
        {
            Packages = new HashSet<Package>();
            Receipts = new HashSet<Receipt>();
        }

        public string Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [MaxLength(20)]
        public string Email { get; set; }

        public ICollection<Package> Packages { get; set; }

        public ICollection<Receipt> Receipts { get; set; }
    }
}