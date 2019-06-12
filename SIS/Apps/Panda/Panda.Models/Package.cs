using System;
using System.ComponentModel.DataAnnotations;

namespace Panda.Models
{
    public class Package
    {
        public string Id { get; set; }

        [MaxLength(20)]
        public string Description { get; set; }

        public decimal Weight { get; set; }

        public string ShippingAddress { get; set; }

        public Status Status { get; set; }

        public DateTime? EstimatedDeliveryDate { get; set; }

        [Required]
        public string RecipientId { get; set; }

        public User Recipient { get; set; }
    }
}