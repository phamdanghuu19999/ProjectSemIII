using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Net.Models.DataModel
{
    public class OrderDetailsViewModel
    {
        public Product Product { get; set; }
        public List<Attributes> Attributes { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double Money { get; set; }
    }
}