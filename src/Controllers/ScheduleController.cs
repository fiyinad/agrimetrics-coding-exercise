using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Orders.Models.Requests;
using Orders.Models.Responses;
using Orders.Response;
using Orders.Services;

namespace Orders.Controllers
{
  /// <inheritdoc />
  [ApiController]
  [ApiVersion("1.0")]
  [Route("api/v{version:apiVersion}/[controller]")]
  public class ScheduleController : ControllerBase
  {
    private readonly ILogger<ScheduleController> _logger;
    private readonly IScheduleService _service;

    public ScheduleController(
      ILogger<ScheduleController> logger,
      IScheduleService service)
    {
      _logger = logger;
      _service = service;
    }

    /// <summary>
    /// Generates a order schedule
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(ScheduleResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Generate([FromBody]ScheduleRequest request)
    {
      var response = await _service.GenerateAsync(request);
      return response.GetResult();
    }
  }
}