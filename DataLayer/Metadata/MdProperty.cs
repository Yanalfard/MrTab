using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace DataLayer.Metadata
{
    public partial class MdProperty
    {
        [Key]
        public int PropertyId { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(150)]
        public string Name { get; set; }
        public int RestaurantId { get; set; }
    }

    [MetadataType(typeof(MdProperty))]
    public class TblProperty
    {

    }
}
