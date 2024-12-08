using Car_Manufacturing.Models;
using Car_Manufacturing.Services.Sales;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Car_Manufacturing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesOrderController : ControllerBase
    {
        private readonly ISalesOrderService _salesOrderService;

        public SalesOrderController(ISalesOrderService salesOrderService)
        {
            _salesOrderService = salesOrderService;
        }

        // GET: api/salesorder
        [HttpGet]
        public async Task<ActionResult<List<SalesOrder>>> GetAllSalesOrders()
        {
            try
            {
                var salesOrders = await _salesOrderService.GetAllSalesOrdersAsync();
                if (salesOrders == null || salesOrders.Count == 0)
                {
                    return NotFound(new { message = "No sales orders found." });
                }
                return Ok(salesOrders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching sales orders.", error = ex.Message });
            }
        }

        // GET: api/salesorder/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<SalesOrder>> GetSalesOrder(int id)
        {
            try
            {
                var salesOrder = await _salesOrderService.GetSalesOrderByIdAsync(id);
                if (salesOrder == null)
                {
                    return NotFound(new { message = $"Sales order with ID {id} not found." });
                }
                return Ok(salesOrder);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred while fetching the sales order with ID {id}.", error = ex.Message });
            }
        }

        // POST: api/salesorder
        [HttpPost]
        public async Task<ActionResult<SalesOrder>> CreateSalesOrder([FromBody] SalesOrder salesOrder)
        {
            if (salesOrder == null)
            {
                return BadRequest(new { message = "Sales order data is required." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid data provided.", errors = ModelState });
            }

            try
            {
                var createdSalesOrder = await _salesOrderService.CreateSalesOrderAsync(salesOrder);
                return CreatedAtAction(nameof(GetSalesOrder), new { id = createdSalesOrder.OrderId }, createdSalesOrder);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the sales order.", error = ex.Message });
            }
        }

        // PUT: api/salesorder/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<SalesOrder>> UpdateSalesOrder(int id, [FromBody] SalesOrder salesOrder)
        {
            if (salesOrder == null)
            {
                return BadRequest(new { message = "Sales order data is required." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid data provided.", errors = ModelState });
            }

            try
            {
                var updatedSalesOrder = await _salesOrderService.UpdateSalesOrderAsync(id, salesOrder);
                if (updatedSalesOrder == null)
                {
                    return NotFound(new { message = $"Sales order with ID {id} not found." });
                }
                return Ok(updatedSalesOrder);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred while updating the sales order with ID {id}.", error = ex.Message });
            }
        }

        // DELETE: api/salesorder/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalesOrder(int id)
        {
            try
            {
                var result = await _salesOrderService.DeleteSalesOrderAsync(id);
                if (!result)
                {
                    return NotFound(new { message = $"Sales order with ID {id} not found." });
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred while deleting the sales order with ID {id}.", error = ex.Message });
            }
        }
    }
}
