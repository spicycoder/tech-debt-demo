using Microsoft.AspNetCore.Mvc;

namespace DocumentRetrievalService.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DocumentController : ControllerBase
{
    [HttpGet("report")]
    public async Task<IActionResult> GetReport()
    {
        return Ok();
    }
}
