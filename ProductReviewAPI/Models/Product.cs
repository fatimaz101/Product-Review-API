﻿using System.ComponentModel.DataAnnotations;

namespace ProductReviewAPI.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public ICollection<Review> Reviews { get; set; }



    }
}
