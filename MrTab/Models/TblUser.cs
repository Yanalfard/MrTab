using System;
using System.Collections.Generic;

#nullable disable

namespace MrTab.Models
{
    public partial class TblUser
    {
        public TblUser()
        {
            TblComments = new HashSet<TblComment>();
            TblRestaurants = new HashSet<TblRestaurant>();
        }

        public int UserId { get; set; }
        public string Name { get; set; }
        public string TellNo { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public bool IsActive { get; set; }
        public string Auth { get; set; }
        public string ImageUrl { get; set; }

        public virtual TblRole Role { get; set; }
        public virtual ICollection<TblComment> TblComments { get; set; }
        public virtual ICollection<TblRestaurant> TblRestaurants { get; set; }
    }
}
