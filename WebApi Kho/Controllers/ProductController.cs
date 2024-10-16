using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebApi_Kho.Models;
using WebApi_Kho.Databases;
using Microsoft.EntityFrameworkCore;

namespace WebApi_Kho.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly DB context;

        public ProductController( DB context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
        {
            return await context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(ProductDTO productDTO)
        {
            var product = new Product()
            {
                Name = productDTO.Name,
                Stock = productDTO.Stock,
                Price = productDTO.Price,
                Description = productDTO.Description,
                ImageUrl = productDTO.ImageUrl,
                CategoryId = productDTO.CategoryId,
                SupplierId = productDTO.SupplierId
                
            };

            context.Products.Add(product);

            await context.SaveChangesAsync();

            return Ok(product);
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await context.Products.FindAsync(id);

            if (product == null) return NotFound();

            return Ok(product);
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            var product = await context.Products.FindAsync(id);

            if (product == null) return NotFound();

            context.Products.Remove(product);

            await context.SaveChangesAsync();

            return Ok("Xóa thành công sản phẩm");

        }

        [HttpPut("{id}")]

        public async Task<ActionResult<Product>> UpdateProduct(int id, ProductDTO productdto)
        {
            var p = await context.Products.FindAsync(id);

            if (p == null) return NotFound();

            p.Name = productdto.Name;
            p.Stock = productdto.Stock;
            p.Price = productdto.Price;
            p.Description = productdto.Description;
            p.ImageUrl = productdto.ImageUrl;
            p.CategoryId = productdto.CategoryId;
            p.SupplierId = productdto.SupplierId;
            
            await context.SaveChangesAsync();

            return Ok(p);

        }
    }

    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public int SupplierId { get; set; }
    }
}
