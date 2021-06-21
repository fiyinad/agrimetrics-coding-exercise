using System.Net;
using System.Threading.Tasks;
using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Orders.Controllers;
using Orders.Models.Requests;
using Orders.Models.Responses;
using Orders.Response;
using Orders.Services;
using Xunit;

namespace OrderTests.Controllers
{
  public class ScheduleControllerTests
  {
    private readonly Fixture _fixture = new Fixture();
    private readonly Mock<NullLogger<ScheduleController>> _logger =
      new Mock<NullLogger<ScheduleController>>();
    private readonly Mock<IScheduleService> _service =
      new Mock<IScheduleService>();
    private readonly ScheduleController _controller;
    public ScheduleControllerTests()
    {
      _controller = new ScheduleController(
        _logger.Object,
        _service.Object);
    }

    [Fact]
    public async Task Generate_ReturnsResponseFromService()
    {
      // arrange 
      var serviceResponse =
        new Response<ScheduleResponse>(HttpStatusCode.OK, _fixture.Create<ScheduleResponse>());
      _service.Setup(_ => _.GenerateAsync(It.IsAny<ScheduleRequest>())).ReturnsAsync(serviceResponse);

      // act 
      var response = await _controller.Generate(It.IsAny<ScheduleRequest>());

      // assert
      Assert.NotNull(response);
      Assert.IsType<OkObjectResult>(response);
      Assert.Equal((int)HttpStatusCode.OK, (response as OkObjectResult).StatusCode);
    }
  }
}