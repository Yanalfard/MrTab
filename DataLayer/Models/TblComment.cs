using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DataLayer.Models
{
    public partial class TblComment
    {
        public TblComment()
        {
            InverseAnswer = new HashSet<TblComment>();
        }

        [Key]
        public int CommentId { get; set; }
        [Required]
        [StringLength(500)]
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
        [Column(TypeName = "datetime")]
        public DateTime DateSubmited { get; set; }
        public int RestaurantId { get; set; }
        public int UserId { get; set; }

        [ForeignKey(nameof(AnswerId))]
        [InverseProperty(nameof(TblComment.InverseAnswer))]
        public virtual TblComment Answer { get; set; }
        [ForeignKey(nameof(RestaurantId))]
        [InverseProperty(nameof(TblRestaurant.TblComment))]
        public virtual TblRestaurant Restaurant { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(TblUser.TblComment))]
        public virtual TblUser User { get; set; }
        [InverseProperty(nameof(TblComment.Answer))]
        public virtual ICollection<TblComment> InverseAnswer { get; set; }
    }
}
