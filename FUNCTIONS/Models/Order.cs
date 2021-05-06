using System;
using System.Collections.Generic;
using System.Text;

namespace FUNCTIONS.Models
{
    public class Order
    {
        public long id { get; set; }
        public string email { get; set; }
        public string reference { get; set; } 
        public float amount { get; set; }
    }
}
