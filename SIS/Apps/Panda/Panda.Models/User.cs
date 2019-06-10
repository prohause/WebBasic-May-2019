using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Panda.Models
{
    public class User
    {
        public User()
        {
            Packages = new List<Package>();
            Receipts = new List<Receipt>();
        }

        public string Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string Username { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public List<Package> Packages { get; set; }

        public List<Receipt> Receipts { get; set; }
    }

    //• Id - a GUID String, Primary Key
    //• Username - a string with min length 5 and max length 20 (required)
    //• Email - a string with min length 5 and max length 20 (required)
    //• Password - a string – hashed in the database(required)
    //• Packages – a Collection of type Packages
    //• Receipts – a Collection of type Receipts
}