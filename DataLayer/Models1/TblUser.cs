using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DataLayer.Models1
{
    [Table("TblUser", Schema = "dbo")]
    public partial class TblUser
    {
        public TblUser()
        {
            TblComment = new HashSet<TblComment>();
            TblRestaurant = new HashSet<TblRestaurant>();
        }

        [Key]
        public int UserId { get; set; }
        [StringLength(150)]
        public string Name { get; set; }
        [Required]
        [StringLength(13)]
        public string TellNo { get; set; }
        [Required]
        [StringLength(64)]
        public string Password { get; set; }
        public int RoleId { get; set; }
        public bool IsActive { get; set; }
        [StringLength(256)]
        public string Auth { get; set; }
        [StringLength(500)]
        public string ImageUrl { get; set; }

        [ForeignKey(nameof(RoleId))]
        [InverseProperty(nameof(TblRole.TblUser))]
        public virtual TblRole Role { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<TblComment> TblComment { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<TblRestaurant> TblRestaurant { get; set; }
    }
}
