using AutoPostOffice.API.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AutoPostOffice.API.Models
{
    public class OrderCreationModel : IValidatableObject
    {
        [Required]
        public List<Product> Products { get; set; }

        public string Description { get; set; }

        [Required]
        [Range(0.0, double.MaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
        public decimal? Cost { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]{4}\-\d{5}$", ErrorMessage = "Incorrect Post number.")]
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

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Products.Count == 0)
            {
                yield return new ValidationResult("There must be at least one Product in Order.");
            }

            int prodCount = 0;
            foreach (var p in Products)
            {
                prodCount = (int)(prodCount + p.Amount);
            }

            if (prodCount > 10)
            {
                yield return new ValidationResult("The amount of product must be less that 10.");
            }
        }
    }
}
