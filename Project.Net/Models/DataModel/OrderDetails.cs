using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project.Net.Models.DataModel
{
    public class OrderDetails
    {
        [Column(Order = 0), Key]
        public int OrderId { get; set; }
        [Column(Order = 1), Key]
        public int ProductId { get; set; }
        [Column(Order = 2), Key]
        public string Attributes { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

        [ForeignKey("OrderId")]
        public Order Orders { get; set; }
        [ForeignKey("ProductId")]
        public Product Products { get; set; }

    }
}