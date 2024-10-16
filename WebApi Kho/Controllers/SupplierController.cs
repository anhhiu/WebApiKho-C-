using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Data;
using WebApi_Kho.Databases;

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
        public async Task<ActionResult<IEnumerable<Models.Supplier>>> GetSuppiler()
        {
            return await context.Suppliers.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Models.Supplier>> CreateSupplier(SupplierDTO supplierDTO)
        {
            Models.Supplier supplier = new Models.Supplier()
            {
                Name = supplierDTO.Name,
                Email = supplierDTO.Email,
                Phone = supplierDTO.Phone,
                Address = supplierDTO.Address,

            };

            context.Suppliers.Add(supplier);

            await context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSuppiler),new {id = supplier.Id}, supplier);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Supplier>> GetSuppilerById(int id)
        {
            var supplier = await context.Suppliers.FindAsync(id);

            if (supplier == null) return NotFound();
            
            return Ok(supplier);

        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<Models.Supplier>> DeleteSupplier(int id)
        {
            var supplier =await context.Suppliers.FindAsync(id);

            if (supplier == null) return NotFound();

            context.Suppliers.Remove(supplier);

            await context.SaveChangesAsync();

            return Ok("Xóa thành công");
        }

        [HttpPut("{id}")]
        

        public async Task<ActionResult<Models.Supplier>> UpdateSupplier(int id, SupplierDTO supplierDTO)
        {
            var supplier = await context.Suppliers.FindAsync(id);

            if (supplier == null) return NotFound();

            supplier.Name = supplierDTO.Name;
            supplier.Email = supplierDTO.Email;
            supplier.Phone = supplierDTO.Phone;
            supplier.Address = supplierDTO.Address;

            await context.SaveChangesAsync();

            return Ok(supplier);

        }

    }

    public class SupplierDTO
    {
        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

      
        public string Phone { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;
    }
}
