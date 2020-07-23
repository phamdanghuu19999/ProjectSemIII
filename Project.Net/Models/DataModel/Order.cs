using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project.Net.Models.DataModel
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        
        public DateTime Created { get; set; }

        public byte Status { get; set; }
        [ForeignKey("UserId")]
        public User Users { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }
    }
}