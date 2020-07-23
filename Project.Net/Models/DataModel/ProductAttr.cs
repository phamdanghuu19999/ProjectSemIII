using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project.Net.Models.DataModel
{
    public class ProductAttr
    {
        [Column(Order = 0),Key]
        [Required]
        public int ProductId { get; set; }
        [Column(Order = 1), Key]
        [Required]
        public int AttrId { get; set; }
		public float PriceByColor { get; set; }
		[ForeignKey("ProductId")]
        public Product Products { get; set; }
        [ForeignKey("AttrId")]
        public virtual Attributes Attribute { get; set; }
    }
}