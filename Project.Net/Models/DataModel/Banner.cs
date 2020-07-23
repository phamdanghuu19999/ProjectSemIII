using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project.Net.Models.DataModel
{
    public class Banner
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        [Display(Name="Note")]
        public int NoteId { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public bool Status { get; set; }

        public DateTime? Created_at { get; set; }
        public DateTime? Update_at { get; set; }
     
        public Note Notes { get; set; }

    }
}