using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DataLayer.Models
{
    public partial class TblDoc
    {
        public TblDoc()
        {
            TblComment = new HashSet<TblComment>();
        }

        [Key]
        public int DocId { get; set; }
        [Required]
        [StringLength(500)]
        public string VideoUrl { get; set; }
        [Required]
        [StringLength(4000)]
        public string BodyHtml { get; set; }
        [StringLength(500)]
        public string MainImage { get; set; }

        [InverseProperty("Doc")]
        public virtual ICollection<TblComment> TblComment { get; set; }
    }
}
