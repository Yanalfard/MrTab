using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DataLayer.Models
{
    [Table("TblReport", Schema = "dbo")]
    public partial class TblReport
    {
        [Key]
        public int ReportId { get; set; }
        [Required]
        [StringLength(200)]
        public string Reason { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public int RestaurantId { get; set; }

        [ForeignKey(nameof(RestaurantId))]
        [InverseProperty(nameof(TblRestaurant.TblReport))]
        public virtual TblRestaurant Restaurant { get; set; }
    }
}
