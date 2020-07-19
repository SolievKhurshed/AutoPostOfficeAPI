using System.ComponentModel.DataAnnotations;

namespace AutoPostOffice.API.Models
{
    public class OrderCancelModel
    {
        [Required]
        public string Description { get; set; }
    }
}
