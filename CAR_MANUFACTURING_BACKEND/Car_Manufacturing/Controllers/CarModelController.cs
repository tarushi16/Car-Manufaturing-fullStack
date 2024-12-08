using Car_Manufacturing.Data;
using Car_Manufacturing.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Car_Manufacturing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]   
    //[Authorize] 
    public class CarModelController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CarModelController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/CarModel
        [HttpGet]
        public ActionResult<IEnumerable<CarModel>> GetCarModels()
        {
            var carModels = _context.CarModels.ToList();
            if (carModels == null || !carModels.Any())
            {
                return NotFound("No car models found.");
            }
            return Ok(carModels);
        }

        // GET: api/CarModel/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CarModel>> GetCarModel(int id)
        {
            var carModel = await _context.CarModels.FindAsync(id);
            if (carModel == null)
            {
                return NotFound($"Car model with ID {id} not found.");
            }
            return Ok(carModel);
        }

        // POST: api/CarModel
        [HttpPost]
        public async Task<ActionResult<CarModel>> CreateCarModel([FromBody] CarModel carModel)
        {
            if (carModel == null)
            {
                return BadRequest("Car model cannot be null.");
            }

            _context.CarModels.Add(carModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCarModel), new { id = carModel.ModelId }, carModel);
        }

        // PUT: api/CarModel/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCarModel(int id, [FromBody] CarModel carModel)
        {
            if (id != carModel.ModelId)
            {
                return BadRequest("Car model ID mismatch.");
            }

            var existingCarModel = await _context.CarModels.FindAsync(id);
            if (existingCarModel == null)
            {
                return NotFound($"Car model with ID {id} not found.");
            }

            // Update the properties of the existing car model
            existingCarModel.ModelName = carModel.ModelName;
            existingCarModel.EngineType = carModel.EngineType;
            existingCarModel.FuelEfficiency = carModel.FuelEfficiency;
            existingCarModel.DesignFeatures = carModel.DesignFeatures;
            existingCarModel.ProductionCost = carModel.ProductionCost;
            existingCarModel.Status = carModel.Status;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.CarModels.Any(c => c.ModelId == id))
                {
                    return NotFound($"Car model with ID {id} not found.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent(); // Successful update, no content returned
        }

        // DELETE: api/CarModel/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] // Only Admin can delete
        public async Task<IActionResult> DeleteCarModel(int id)
        {
            var carModel = await _context.CarModels.FindAsync(id);
            if (carModel == null)
            {
                return NotFound($"Car model with ID {id} not found.");
            }

            _context.CarModels.Remove(carModel);
            await _context.SaveChangesAsync();

            return NoContent(); // Successful delete, no content returned
        }
    }
}
