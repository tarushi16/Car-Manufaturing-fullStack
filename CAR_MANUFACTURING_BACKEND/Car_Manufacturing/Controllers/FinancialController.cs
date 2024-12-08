using Car_Manufacturing.Models;
using Car_Manufacturing.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Car_Manufacturing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinancialController : ControllerBase
    {
        private readonly IFinancialService _financialService;

        public FinancialController(IFinancialService financialService)
        {
            _financialService = financialService;
        }

        [HttpGet("{financeId}")]
        public async Task<ActionResult<Finance>> GetFinanceById(int financeId)
        {
            var finance = await _financialService.GetFinanceByIdAsync(financeId);
            if (finance == null)
            {
                return NotFound(new { message = $"Finance record with ID {financeId} not found." });
            }
            return Ok(finance);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Finance>>> GetAllFinances()
        {
            var finances = await _financialService.GetAllFinancesAsync();
            return Ok(finances);
        }

        [HttpPost]
        public async Task<ActionResult<Finance>> CreateFinance([FromBody] Finance finance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdFinance = await _financialService.CreateFinanceAsync(finance);
            return CreatedAtAction(nameof(GetFinanceById), new { financeId = createdFinance.FinanceId }, createdFinance);
        }

        [HttpPut("{financeId}")]
        public async Task<ActionResult<Finance>> UpdateFinance(int financeId, [FromBody] Finance updatedFinance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var finance = await _financialService.UpdateFinanceAsync(financeId, updatedFinance);
            if (finance == null)
            {
                return NotFound(new { message = $"Finance record with ID {financeId} not found." });
            }
            return Ok(finance);
        }

        [HttpDelete("{financeId}")]
        public async Task<ActionResult> DeleteFinance(int financeId)
        {
            var success = await _financialService.DeleteFinanceAsync(financeId);
            if (!success)
            {
                return NotFound(new { message = $"Finance record with ID {financeId} not found." });
            }
            return NoContent();
        }
    }
}
