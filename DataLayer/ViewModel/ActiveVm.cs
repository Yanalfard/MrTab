using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.ViewModel
{
    public class ActiveVm
    {
        [Display(Name = "کد فعال سازی  ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(5)]
        public string Id { get; set; }
        public string Tell { get; set; }
    }

}
