using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project.Net.Models.DataModel
{
    public class Product
    {
        [Key]
        [Required]
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int SupplierId { get; set; }
        [Required]
        public string Image { get; set; }
        public string ImageList { get; set; }
        public string Description { get; set; }
        public float? Price { get; set; }
        public float? SalePrice { get; set; }
        public bool Status { get; set; }
        [Display(Name ="Note")]
        public DateTime? Created_at { get; set; }
        public DateTime? Update_at { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Categories { get; set; }
        [ForeignKey("SupplierId")]
        public  Supplier Suppliers { get; set; }

        public ICollection<OrderDetails> OrderDetails { get; set; }
        public  ICollection<ProductAttr> ProductAttrs { get; set; }
        public virtual ICollection<ProductNote> ProductNotes { get; set; }

    }
}