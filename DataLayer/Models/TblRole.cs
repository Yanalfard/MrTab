using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DataLayer.Models
{
    public partial class TblRole
    {
        public TblRole()
        {
            TblUser = new HashSet<TblUser>();
        }

        [Key]
        public int RoleId { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [InverseProperty("Role")]
        public virtual ICollection<TblUser> TblUser { get; set; }
    }
}
