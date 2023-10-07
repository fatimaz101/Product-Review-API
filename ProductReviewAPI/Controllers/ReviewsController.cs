using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductReviewAPI.Data;
using ProductReviewAPI.DTOs;
using ProductReviewAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductReviewAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public ReviewsController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/Reviews
        [HttpGet]
        public IActionResult Get()
        {


            var reviews = _context.Reviews.Include(r => r.Product).ToList();

            return StatusCode(200, reviews);
               
            
        }

        // GET api/Reviews
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var review = _context.Reviews.Include(r => r.Product).FirstOrDefault(r => r.Id == id);


                return Ok( review);
          
        }



        //GET api/Reviews/search/product/{productId}
        [HttpGet("search/product/{id}")]
        public IActionResult SearchByProductId(int id)
        {
            // Perform search logic based on the provided keyword
            // Return search results
            var reviewsList = _context.Reviews.Include(r => r.Product).Where(r => r.ProductId == id).ToList();


            return StatusCode(200 , reviewsList);


        }









        // POST api/<ReviewsController>
        [HttpPost]
        public IActionResult Post([FromBody] Review review)

        {
            //var product = 
            //    _context.Reviews.Include(r=> r.Pro)

            //var product = _context.Products.Where(p => p.Id == review.ProductId).FirstOrDefault();
            //review.Product = roduct;

            //if (review.ProductId != null) 
            //{
            //    var productReview = new ReviewDTO
            //    {

            //        Text = review.Text,
            //        Rating = review.Rating,
            //        ProductId = review.ProductId,

            //        Product = _context.Products.Find(review.ProductId);




            //}else
            //{
            //    return NotFound();
            //}





            //    _context.Reviews.Add(productReview);
            //    _context.SaveChanges();
            //    return StatusCode(201, productReview);
            return null; 
        }

        // PUT api/<ReviewsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Review review)
        {


            var updatedReview = _context.Reviews.Where(r => r.Id == id).First();



            try
            {



                updatedReview.Id = review.Id;
                updatedReview.Text = review.Text;
                updatedReview.Rating = review.Rating;
                updatedReview.ProductId = review.ProductId;
                updatedReview.Product = review.Product;




                _context.SaveChanges();
                return StatusCode(200, review);
            }
            catch
            {
                return NotFound();
            }



            }

        // DELETE api/<ReviewsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {

            var review = _context.Reviews.Where(r => r.Id == id).First();
            _context.Reviews.Remove(review);
            _context.SaveChanges();
            return StatusCode(200, NotFound());
        }
    }
}
