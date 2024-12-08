using Car_Manufacturing.Models;
using Car_Manufacturing.Services.Reporting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Car_Manufacturing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        // GET: api/report
        [HttpGet]
        public async Task<IActionResult> GetAllReports()
        {
            try
            {
                var reports = await _reportService.GetAllReportsAsync();
                return Ok(reports);
            }
            catch (Exception ex)
            {
                // Log the error here
                return StatusCode(500, new { message = "An error occurred while fetching the reports." });
            }
        }

        // GET: api/report/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReport(int id)
        {
            try
            {
                var report = await _reportService.GetReportByIdAsync(id);
                if (report == null)
                {
                    return NotFound(new { message = $"Report with ID {id} not found." });
                }
                return Ok(report);
            }
            catch (Exception ex)
            {
                // Log the error here
                return StatusCode(500, new { message = $"An error occurred while fetching the report with ID {id}." });
            }
        }

        // POST: api/report
        [HttpPost]
        public async Task<IActionResult> CreateReport([FromBody] Report report)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid data provided." });
            }

            try
            {
                var createdReport = await _reportService.CreateReportAsync(report);
                return CreatedAtAction(nameof(GetReport), new { id = createdReport.ReportId }, createdReport);
            }
            catch (Exception ex)
            {
                // Log the error here
                return StatusCode(500, new { message = "An error occurred while creating the report." });
            }
        }

        // PUT: api/report/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReport(int id, [FromBody] Report report)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid data provided." });
            }

            try
            {
                var updatedReport = await _reportService.UpdateReportAsync(id, report);
                if (updatedReport == null)
                {
                    return NotFound(new { message = $"Report with ID {id} not found." });
                }
                return Ok(updatedReport);
            }
            catch (Exception ex)
            {
                // Log the error here
                return StatusCode(500, new { message = $"An error occurred while updating the report with ID {id}." });
            }
        }

        // DELETE: api/report/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReport(int id)
        {
            try
            {
                var result = await _reportService.DeleteReportAsync(id);
                if (!result)
                {
                    return NotFound(new { message = $"Report with ID {id} not found." });
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the error here

                return StatusCode(500, new { message = $"An error occurred while deleting the report with ID {id}." });
            }
        }
    }
}
