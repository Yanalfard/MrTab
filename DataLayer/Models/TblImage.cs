using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DataLayer.Models
{
    [Table("TblImage", Schema = "dbo")]
    public partial class TblImage
    {
        [Key]
        public int ImageId { get; set; }
        [StringLength(1000)]
        public string Url { get; set; }
        public int Status { get; set; }
        public int RestaurantId { get; set; }
        public bool IsValid { get; set; }

        [ForeignKey(nameof(RestaurantId))]
        [InverseProperty(nameof(TblRestaurant.TblImage))]
        public virtual TblRestaurant Restaurant { get; set; }
    }
}
