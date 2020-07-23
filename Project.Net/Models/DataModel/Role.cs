using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Net.Models.DataModel
{
    public class Role
    {
        [Key]
        
        public string RoleId { get; set; }
        [Required]
        public string  RoleName { get; set; }
        [Required]
        public bool Status { get; set; }
    }
}