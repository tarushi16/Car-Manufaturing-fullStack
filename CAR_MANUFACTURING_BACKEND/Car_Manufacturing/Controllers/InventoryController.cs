using Car_Manufacturing.Models;
using Car_Manufacturing.Services.Inventory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Car_Manufacturing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;
        private readonly ILogger<InventoryController> _logger;

        public InventoryController(IInventoryService inventoryService, ILogger<InventoryController> logger)
        {
            _inventoryService = inventoryService;
            _logger = logger;
        }

        // GET: api/Inventory
        [HttpGet]
        public async Task<ActionResult<List<InventoryModel>>> GetInventory()
        {
            try
            {
                var inventory = await _inventoryService.GetAllInventoryItemsAsync();
                return Ok(inventory);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching inventory items.");
                return StatusCode(500, new { message = "Error fetching inventory items.", error = ex.Message });
            }
        }

        // GET: api/Inventory/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryModel>> GetInventoryById(int id)
        {
            try
            {
                var inventory = await _inventoryService.GetInventoryItemByIdAsync(id);
                if (inventory == null)
                {
                    return NotFound(new { message = $"Inventory item with ID {id} not found." });
                }

                return Ok(inventory);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error fetching inventory item with ID {id}.");
                return StatusCode(500, new { message = "Error fetching the inventory item.", error = ex.Message });
            }
        }

        // POST: api/Inventory
        [HttpPost]
        public async Task<ActionResult<InventoryModel>> AddInventory([FromBody] InventoryModel inventory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdInventory = await _inventoryService.CreateInventoryItemAsync(inventory);
                return CreatedAtAction(nameof(GetInventoryById), new { id = createdInventory.InventoryId }, createdInventory);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding inventory item.");
                return StatusCode(500, new { message = "Error adding inventory item.", error = ex.Message });
            }
        }

        // PUT: api/Inventory/5
        [HttpPut("{id}")]
        public async Task<ActionResult<InventoryModel>> UpdateInventory(int id, [FromBody] InventoryModel inventory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedInventory = await _inventoryService.UpdateInventoryItemAsync(id, inventory);
                if (updatedInventory == null)
                {
                    return NotFound(new { message = $"Inventory item with ID {id} not found." });
                }

                return Ok(updatedInventory);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating inventory item with ID {id}.");
                return StatusCode(500, new { message = "Error updating inventory item.", error = ex.Message });
            }
        }

        // DELETE: api/Inventory/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteInventory(int id)
        {
            try
            {
                var isDeleted = await _inventoryService.DeleteInventoryItemAsync(id);
                if (!isDeleted)
                {
                    return NotFound(new { message = $"Inventory item with ID {id} not found." });
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting inventory item with ID {id}.");
                return StatusCode(500, new { message = "Error deleting inventory item.", error = ex.Message });
            }
        }
    }
}
