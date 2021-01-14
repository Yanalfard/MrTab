using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DataLayer.Models
{
    [Table("TblImage")]
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
        [InverseProperty(nameof(TblRestaurant.TblImages))]
        public virtual TblRestaurant Restaurant { get; set; }
    }
}
