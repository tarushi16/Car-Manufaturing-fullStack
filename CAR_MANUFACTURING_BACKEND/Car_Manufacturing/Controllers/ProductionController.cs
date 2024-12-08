using Car_Manufacturing.Models;
using Car_Manufacturing.Services.Production;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Car_Manufacturing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionController : ControllerBase
    {
        private readonly IProductionService _productionService;
        private readonly ILogger<ProductionController> _logger;

        public ProductionController(IProductionService productionService, ILogger<ProductionController> logger)
        {
            _productionService = productionService;
            _logger = logger;
        }

        // GET: api/production
        [HttpGet]
        public async Task<IActionResult> GetAllProductionOrders()
        {
            try
            {
                var productionOrders = await _productionService.GetAllProductionOrdersAsync();
                return Ok(productionOrders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all production orders.");
                return StatusCode(500, new { message = "Internal server error while retrieving production orders." });
            }
        }

        // GET: api/production/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductionOrder(int id)
        {
            try
            {
                var productionOrder = await _productionService.GetProductionOrderByIdAsync(id);
                if (productionOrder == null)
                {
                    return NotFound(new { message = $"Production order with ID {id} not found." });
                }
                return Ok(productionOrder);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving production order with ID {id}.");
                return StatusCode(500, new { message = $"Internal server error while retrieving production order with ID {id}." });
            }
        }

        // POST: api/production
        [HttpPost]
        public async Task<IActionResult> CreateProductionOrder([FromBody] ProductionOrder productionOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdOrder = await _productionService.CreateProductionOrderAsync(productionOrder);
                return CreatedAtAction(nameof(GetProductionOrder), new { id = createdOrder.OrderId }, createdOrder);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating a production order.");
                return StatusCode(500, new { message = "Internal server error while creating production order." });
            }
        }

        // PUT: api/production/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductionOrder(int id, [FromBody] ProductionOrder productionOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedOrder = await _productionService.UpdateProductionOrderAsync(id, productionOrder);
                if (updatedOrder == null)
                {
                    return NotFound(new { message = $"Production order with ID {id} not found." });
                }
                return Ok(updatedOrder);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating production order with ID {id}.");
                return StatusCode(500, new { message = $"Internal server error while updating production order with ID {id}." });
            }
        }

        // DELETE: api/production/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductionOrder(int id)
        {
            try
            {
                var result = await _productionService.DeleteProductionOrderAsync(id);
                if (!result)
                {
                    return NotFound(new { message = $"Production order with ID {id} not found." });
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting production order with ID {id}.");
                return StatusCode(500, new { message = $"Internal server error while deleting production order with ID {id}." });
            }
        }
    }
}
