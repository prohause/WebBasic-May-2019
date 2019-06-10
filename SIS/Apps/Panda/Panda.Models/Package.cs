using System;
using System.ComponentModel.DataAnnotations;

namespace Panda.Models
{
    public class Package
    {
        public string Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string Description { get; set; }

        public double Weight { get; set; }

        public string Address { get; set; }

        public Status Status { get; set; }

        public DateTime EstimatedDeliveryDate { get; set; }

        [Required]
        public string RecipientId { get; set; }

        public User Recipient { get; set; }
    }

    //• Id - a GUID String, Primary Key
    //• Description - a string a string with min length 5 and max length 20 (required)
    //• Weight - a floating-point number
    //• Shipping Address - a string
    //• Status - can be one of the following values("Pending", "Delivered")
    //• Estimated Delivery Date - a DateTime object
    //• RecipientId - GUID foreign key(required)
    //• Recipient - a User object
}