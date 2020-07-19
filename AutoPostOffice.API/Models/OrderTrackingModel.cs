using AutoPostOffice.API.Data.Entities;
using System;
using System.Collections.Generic;

namespace AutoPostOffice.API.Models
{
    public class OrderTrackingModel
    {
        public int ChangesCount { get; set; }
        public int OrderNumber { get; set; }
        public DateTime Modified { get; set; }
        public string ChangeType { get; set; }
        public OrderStatus Status { get; set; }
        public string Description { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public decimal? Cost { get; set; }
        public string PostNumber { get; set; }
        public string RecipientPhoneNumber { get; set; }
        public string RecipientFirstName { get; set; }
        public string RecipientLastName { get; set; }
        public string RecipientMiddleName { get; set; }
    }
}
