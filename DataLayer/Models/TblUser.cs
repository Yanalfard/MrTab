using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DataLayer.Models
{
    [Table("TblUser")]
    public partial class TblUser
    {
        public TblUser()
        {
            TblComments = new HashSet<TblComment>();
            TblRestaurants = new HashSet<TblRestaurant>();
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
        [InverseProperty(nameof(TblRole.TblUsers))]
        public virtual TblRole Role { get; set; }
        [InverseProperty(nameof(TblComment.User))]
        public virtual ICollection<TblComment> TblComments { get; set; }
        [InverseProperty(nameof(TblRestaurant.User))]
        public virtual ICollection<TblRestaurant> TblRestaurants { get; set; }
    }
}
