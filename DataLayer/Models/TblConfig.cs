using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace DataLayer.Models
{
    [Table("TblConfig")]
    public partial class TblConfig
    {
        [Key]
        [StringLength(100)]
        public string Keyword { get; set; }
        [StringLength(1000)]
        public string Value { get; set; }
    }
}
