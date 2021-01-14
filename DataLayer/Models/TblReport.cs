using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DataLayer.Models
{
    [Table("TblReport")]
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
        [InverseProperty(nameof(TblRestaurant.TblReports))]
        public virtual TblRestaurant Restaurant { get; set; }
    }
}
