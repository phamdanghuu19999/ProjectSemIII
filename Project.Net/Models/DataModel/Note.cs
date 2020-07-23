using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Net.Models.DataModel
{
    public class Note
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public int Type { get; set; }
        [Required]
        public string AttrName { get; set; }
        public ICollection<Banner> Banners { get; set; }
        public ICollection<ProductNote> ProductNotes { get; set; }
    }
}