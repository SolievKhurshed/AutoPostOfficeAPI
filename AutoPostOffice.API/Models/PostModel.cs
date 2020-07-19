using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoPostOffice.API.Models
{
    public class PostModel
    {
        public string Number { get; set; }
        public string Address { get; set; }
        public bool Status { get; set; }
    }
}
