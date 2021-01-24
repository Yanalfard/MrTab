using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModel
{
    public class SendCommentVm
    {
        public int RestaurantId { get; set; }
        public int UserId { get; set; }
        public short Rating { get; set; }
        public int RateCount { get; set; }
        public short ExpenseRate { get; set; }
        public short QualityRate { get; set; }
        public short ServiceRate { get; set; }
        public short DecorRate { get; set; }
        public short QualityPerPriceRate { get; set; }
        public string Comment { get; set; }
    }
}
