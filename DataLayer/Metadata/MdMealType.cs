using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace DataLayer.Metadata
{
    public partial class MdMealType
    {
        [Key]
        public int MealTypeId { get; set; }
        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(50)]
        public string Name { get; set; }
        public int RestaurantId { get; set; }
    }

    [MetadataType(typeof(MdMealType))]
    public class TblMealType
    {

    }
}
