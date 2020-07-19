﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoPostOffice.API.Data.Entities
{
    public class Post
    {
        [Required]
        [RegularExpression(@"^[A-Z]{4}\-\d{5}$", ErrorMessage = "Incorrect Post number.")]
        public string Number { get; set; }

        [Required]
        [MaxLength(300)]
        public string Address { get; set; }

        [Required]
        public bool Status { get; set; }
    }
}
