using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project.Net.Models.DataModel
{
    public class AttributeType
    {
        [Key]
        
        public int AttrTypeId { get; set; }
        [Required]
        public string TypeName { get; set; }
        [Required]
        public int OrderBy { get; set; }
        [Required]
        public bool Status { get; set; }
        public ICollection<Attributes> Attributes { get; set; }
    }
}