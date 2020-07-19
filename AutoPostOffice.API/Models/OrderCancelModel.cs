using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoPostOffice.API.Models
{
    public class OrderCancelModel
    {
        [Required]
        public string Description { get; set; }
    }
}
