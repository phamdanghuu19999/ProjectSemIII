using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Net.Models.DataModel
{
    public class Supplier
    {
        [Key]
        
        public int SupplierId { get; set; }
        [Required]
        public string SupplierName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Logo { get; set; }
        [Required]
        public bool Status { get; set; }
        public DateTime? Created_at { get; set; }
        public DateTime? Update_at { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}