// QualityControlController.cs
using Car_Manufacturing.Models;
using Car_Manufacturing.Services.QualityControl;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Car_Manufacturing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QualityControlController : ControllerBase
    {
        private readonly IQualityControlService _qualityControlService;

        public QualityControlController(IQualityControlService qualityControlService)
        {
            _qualityControlService = qualityControlService;
        }

        // GET: api/qualitycontrol
        [HttpGet]
        public async Task<IActionResult> GetAllQualityReports()
        {
            try
            {
                var qualityReports = await _qualityControlService.GetAllQualityReportsAsync();
                return Ok(qualityReports);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", details = ex.Message });
            }
        }

        // GET: api/qualitycontrol/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetQualityReport(int id)
        {
            try
            {
                var qualityReport = await _qualityControlService.GetQualityReportByIdAsync(id);
                if (qualityReport == null)
                {
                    return NotFound(new { message = "Quality report not found" });
                }
                return Ok(qualityReport);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", details = ex.Message });
            }
        }

        // POST: api/qualitycontrol
        [HttpPost]
        public async Task<IActionResult> CreateQualityReport([FromBody] QualityReport qualityReport)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdReport = await _qualityControlService.CreateQualityReportAsync(qualityReport);
                return CreatedAtAction(nameof(GetQualityReport), new { id = createdReport.ReportId }, createdReport);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", details = ex.Message });
            }
        }

        // PUT: api/qualitycontrol/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQualityReport(int id, [FromBody] QualityReport qualityReport)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedReport = await _qualityControlService.UpdateQualityReportAsync(id, qualityReport);
                if (updatedReport == null)
                {
                    return NotFound(new { message = "Quality report not found" });
                }
                return Ok(updatedReport);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", details = ex.Message });
            }
        }

        // DELETE: api/qualitycontrol/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQualityReport(int id)
        {
            try
            {
                var result = await _qualityControlService.DeleteQualityReportAsync(id);
                if (!result)
                {
                    return NotFound(new { message = "Quality report not found" });
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", details = ex.Message });
            }
        }
    }
}
