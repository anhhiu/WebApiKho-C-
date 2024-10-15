using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi_Kho.Databases;
using WebApi_Kho.Models;

namespace WebApi_Kho.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly DB context;

        public SupplierController( DB context) 
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Supplier>>> GetSuppiler()
        {
            return await context.Suppliers.ToListAsync();
        }

        [HttpPost]

        public async Task<ActionResult<Supplier>> CreateSupplier(Supplier supplier)
        {
            supplier.CreatedAt = DateTime.UtcNow;

            context.Suppliers.Add(supplier);

            await context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSuppiler),new {id = supplier.Id}, supplier);
        }


    }
}
