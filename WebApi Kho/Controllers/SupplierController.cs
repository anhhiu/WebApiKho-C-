using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
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

        [HttpGet("{id}")]
        public async Task<ActionResult<Supplier>> GetSuppilerById(int id)
        {
            var supplier = await context.Suppliers.FindAsync(id);

            if (supplier == null) return NotFound();
            
            return Ok(supplier);

        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<Supplier>> DeleteSupplier(int id)
        {
            var supplier =await context.Suppliers.FindAsync(id);

            if (supplier == null) return NotFound();

            context.Suppliers.Remove(supplier);

            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        

        public async Task<ActionResult<Supplier>> UpdateSupplier(int id, Supplier supplier)
        {
            if (id != supplier.Id) return BadRequest();

            context.Entry(supplier).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
              if  (!SupplierExits(id)) return BadRequest();
              else throw;
            }

            return Ok(supplier);

        }
        private bool SupplierExits(int id) => context.Suppliers.Any(x => x.Id == id);

    }
}
