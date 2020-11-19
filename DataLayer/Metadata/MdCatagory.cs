using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace DataLayer.Metadata
{
    public partial class MdCatagory
    {
        [Key]
        public int CatagoryId { get; set; }
        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(150)]
        public string Name { get; set; }
    }

    [MetadataType(typeof(MdCatagory))]
    public class TblCatagory
    {

    }
}
