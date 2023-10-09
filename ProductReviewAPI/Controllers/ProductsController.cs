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
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductsController (ApplicationDbContext context)
        {
            _context = context;
        }




        // GET: api/<ProductsController>
        [HttpGet]
        public IActionResult Get([FromQuery] int? maxprice)
        {
            var products = _context.Products.ToList();
            if(maxprice != null)
            {
                products = _context.Products.Where(p => p.Price <= maxprice).ToList();
            }
           
            return Ok(products);
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var product = _context.Products.Where(p => p.Id == id).FirstOrDefault();

            var productDTO = new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Reviews = _context.Reviews.Include(r => r.Product).Where(r => r.ProductId == id).Select(r => new ReviewDTO
                {
                    Text = r.Text,
                    Rating = r.Rating
                }).ToList()
               

        };
            productDTO.AverageRating =  productDTO.Reviews.Average(r => r.Rating);


            return StatusCode(200, productDTO);
        }

        // POST api/<ProductsController>
        [HttpPost]
        public IActionResult Post([FromBody] Product product)
        {

            
            _context.Products.Add(product);
            _context.SaveChanges();
            return StatusCode(201, product);



        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Product product)
        {

            var updatedProd = _context.Products.Where(p => p.Id == id).First();



            try
            {


                updatedProd.Name = product.Name;
                updatedProd.Price = product.Price;
                




                _context.SaveChanges();
                return StatusCode(200, updatedProd);
            }
            catch
            {
                return NotFound();
            }






        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {

            var product = _context.Products.Where(sng => sng.Id == id).First();
            _context.Products.Remove(product);
            _context.SaveChanges();
            return StatusCode(200, NotFound());




        }
    }
}
