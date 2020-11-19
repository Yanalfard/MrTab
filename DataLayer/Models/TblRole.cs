﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace DataLayer.Models
{
    [Table("TblRole")]
    public partial class TblRole
    {
        public TblRole()
        {
            TblUsers = new HashSet<TblUser>();
        }

        [Key]
        public int RoleId { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [InverseProperty(nameof(TblUser.Role))]
        public virtual ICollection<TblUser> TblUsers { get; set; }
    }
}
