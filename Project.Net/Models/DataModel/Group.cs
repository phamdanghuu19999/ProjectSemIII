using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Net.Models.DataModel
{
    public class Group
    {
        [Key]
        
        public int GroupId { get; set; }
        [Required]
        public string GroupName { get; set; }
        [Required]
        public bool Status { get; set; }

        public ICollection<User> Users { get; set; }
        public ICollection<GroupRoles> GroupRoles { get; set; }
    }
}