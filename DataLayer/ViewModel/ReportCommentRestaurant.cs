using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModel
{
    public class ReportCommentRestaurant
    {
        public int CommentId { get; set; }
        public int RestaurantId { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }

    }
}
