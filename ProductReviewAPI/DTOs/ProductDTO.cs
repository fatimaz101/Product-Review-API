using ProductReviewAPI.Models;

namespace ProductReviewAPI.DTOs
{
    public class ProductDTO
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public double AverageRating { get; set; }

    }
}
