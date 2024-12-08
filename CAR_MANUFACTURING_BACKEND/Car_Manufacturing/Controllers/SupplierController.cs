using Car_Manufacturing.Models;
using Car_Manufacturing.Services.Supplier;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Car_Manufacturing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        // GET: api/supplier
        [HttpGet]
        public async Task<IActionResult> GetAllSuppliers()
        {
            var suppliers = await _supplierService.GetAllSuppliersAsync();
            return Ok(suppliers);
        }

        // GET: api/supplier/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSupplier(int id)
        {
            var supplier = await _supplierService.GetSupplierByIdAsync(id);
            if (supplier == null)
            {
                return NotFound(new { message = $"Supplier with ID {id} not found." });
            }
            return Ok(supplier);
        }

        // POST: api/supplier
        [HttpPost]
        public async Task<IActionResult> CreateSupplier([FromBody] SupplierModel supplier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);  // Ensure the model is valid
            }

            var createdSupplier = await _supplierService.CreateSupplierAsync(supplier);
            return CreatedAtAction(nameof(GetSupplier), new { id = createdSupplier.SupplierId }, createdSupplier);
        }

        // PUT: api/supplier/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSupplier(int id, [FromBody] SupplierModel supplier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);  // Ensure the model is valid
            }

            var updatedSupplier = await _supplierService.UpdateSupplierAsync(id, supplier);
            if (updatedSupplier == null)
            {
                return NotFound(new { message = $"Supplier with ID {id} not found." });
            }
            return Ok(updatedSupplier);
        }

        // DELETE: api/supplier/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplier(int id)
        {
            var result = await _supplierService.DeleteSupplierAsync(id);
            if (!result)
            {
                return NotFound(new { message = $"Supplier with ID {id} not found." });
            }
            return NoContent();
        }
    }
}
