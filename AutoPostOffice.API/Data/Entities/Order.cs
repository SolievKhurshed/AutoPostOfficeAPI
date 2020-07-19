using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoPostOffice.API.Data.Entities
{
    public enum OrderStatus
    {
        Registered = 1,
        InStock = 2,
        IssuedToCourier = 3,
        DeliveredToPostOffice = 4,
        DeliveredToRecipient = 5,
        Cancelled = 6
    }

    public class Order
    {
        [Required]
        public int OrderNumber { get; set; }

        [Required]
        public OrderStatus Status { get; set; }

        public string Description { get; set; }

        [Required]
        public IEnumerable<Product> Products { get; set; }

        [Required]
        [Range(0.0, double.MaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
        public decimal? Cost { get; set; }

        [Required]
        public string PostNumber { get; set; }
        
        [Required]
        [RegularExpression(@"^\+7\d{3}-\d{3}-\d{2}-\d{2}$", ErrorMessage = "Incorrect mobile phone number (allowed +7XXX-XXX-XX-XX, where X is a positive number).")]
        public string RecipientPhoneNumber { get; set; }

        [Required]
        [MaxLength(50)]
        public string RecipientFirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string RecipientLastName { get; set; }

        [MaxLength(50)]
        public string RecipientMiddleName { get; set; }
    }
}
