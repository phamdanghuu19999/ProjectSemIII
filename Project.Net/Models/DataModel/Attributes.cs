using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project.Net.Models.DataModel
{
    public class Attributes
    {
        [Key]
        
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Value { get; set; }
        [Required]
        public int TypeId { get; set; }
        [Required]
        public bool Status { get; set; }
        [ForeignKey("TypeId")]
        public virtual AttributeType AttributeTypes { get; set; }
        public ICollection<ProductAttr> ProductAttrs { get; set; }
    }
}