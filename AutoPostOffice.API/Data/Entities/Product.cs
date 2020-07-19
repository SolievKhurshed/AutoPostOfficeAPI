using System;
using System.ComponentModel.DataAnnotations;

namespace AutoPostOffice.API.Data.Entities
{
    public class Product
    {
        [Required]
        [MaxLength(1500)]
        public string Name { get; set; }

        [Required]
        [Range(1, 10)]
        public int? Amount { get; set; }
    }
}
    