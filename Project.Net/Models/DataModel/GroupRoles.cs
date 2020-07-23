using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project.Net.Models.DataModel
{
    public class GroupRoles
    {
        [Column(Order = 0),Key]
        [Required]
        public int GroupId { get; set; }
        [Column(Order = 1), Key]
        [Required]
        public string BusinessId { get; set; }
        [Column(Order = 2), Key]
        public string RoleId { get; set; }

        [ForeignKey("GroupId")]
        public Group Groups { get; set; }
        [ForeignKey("BusinessId")]
        public Business Business { get; set; }
        [ForeignKey("RoleId")]
        public Role Roles { get; set; }

    }
}