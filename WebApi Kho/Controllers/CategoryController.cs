using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi_Kho.Databases;
using WebApi_Kho.Models;

namespace WebApi_Kho.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly DB context;

        public CategoryController(DB context)
        {
            this.context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Category>> CreateCategory(CategoryDTO categoryDTO)
        {
            var category = new Category()
            {
                Name = categoryDTO.Name,
                CreatedAt = DateTime.UtcNow,
            };

            context.Categories.Add(category);

            await context.SaveChangesAsync();

            return Ok(category);
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<Category>>> GetAllCategory()
        {
            var categories = await context.Categories
                .Include(c => c.Products)
                .ToListAsync();

            var categoryDTOs = categories.Select(c => new CategoryDTO()
            {
                Id = c.Id,
                Name = c.Name,
                Products = c.Products.Select(p => new ProductDTO()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Stock = p.Stock,
                    Price = p.Price,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl,
                    CategoryId = c.Id,
                    SupplierId = p.SupplierId,

                }).ToList()
            }).ToList(); 

            return Ok(categoryDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategoryById(int id)
        {
            var category = await context.Categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            var cateroryDTO = new CategoryDTO()
            {
                Id = category.Id,
                Name = category.Name,
                Products = category.Products.Select(p => new ProductDTO()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Stock = p.Stock,
                    Price = p.Price,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl,
                    SupplierId = p.SupplierId,
                    CategoryId = p.CategoryId,
                }).ToList(),
            };

            return Ok(cateroryDTO);
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<Category>> DeleteCategory(int id)
        {
            var category = await context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            context.Categories.Remove(category);

            await context.SaveChangesAsync();

            return Ok("Xóa thành công danh mục");

        }

        [HttpPut("{id}")]

        public async Task<ActionResult<Category>> UpdateCategory(int id, CategoryDTO categoryDTO)
        {
            var category = await context.Categories.FindAsync(id);

            if (category == null) return NotFound();

            category.Name = categoryDTO.Name;
            category.UpdatedAt = DateTime.UtcNow;

            await context.SaveChangesAsync();

            return Ok(category);
        }
    }

    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<ProductDTO> Products { get; set; } = new List<ProductDTO>();
    }

}
