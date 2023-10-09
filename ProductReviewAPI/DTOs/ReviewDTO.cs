using System.ComponentModel.DataAnnotations;

namespace ProductReviewAPI.DTOs
{
    public class ReviewDTO
    {
        [Key]
      //Took out Id Property to not have it show up in query
        public string Text { get; set; }
        public int Rating { get; set; }
    }
}
