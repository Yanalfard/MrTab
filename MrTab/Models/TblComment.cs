using System;
using System.Collections.Generic;

#nullable disable

namespace MrTab.Models
{
    public partial class TblComment
    {
        public TblComment()
        {
            InverseAnswer = new HashSet<TblComment>();
        }

        public int CommentId { get; set; }
        public string Text { get; set; }
        public int LikeCount { get; set; }
        public bool IsReported { get; set; }
        public short Rate { get; set; }
        public short ExpenseRate { get; set; }
        public short QualityRate { get; set; }
        public short ServiceRate { get; set; }
        public short DecorRate { get; set; }
        public short QualityPerPriceRate { get; set; }
        public int AnswerId { get; set; }
        public DateTime DateSubmited { get; set; }
        public int RestaurantId { get; set; }
        public int UserId { get; set; }

        public virtual TblComment Answer { get; set; }
        public virtual TblRestaurant Restaurant { get; set; }
        public virtual TblUser User { get; set; }
        public virtual ICollection<TblComment> InverseAnswer { get; set; }
    }
}
