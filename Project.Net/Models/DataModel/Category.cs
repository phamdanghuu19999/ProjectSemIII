using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Net.Models.DataModel
{
    public class Category
    {
        [Key]
        
        public int CategoryId { get; set; }
        [Required]
        public string CategoryName { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public int OrderBy { get; set; }
        [Required]
        public bool Status { get; set; }
        
        public DateTime? Created_at { get; set; }
        public DateTime? Update_at { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}