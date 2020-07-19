using AutoPostOffice.API.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoPostOffice.API.Models
{
    public class OrderModel
    {
        public int OrderNumber { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public List<Product> Products { get; set; }
        public decimal Cost { get; set; }
        public string PostNumber { get; set; }
        public string RecipientPhoneNumber { get; set; }
        public string RecipientFullName { get; set; }
    }
}
