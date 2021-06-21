using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Orders.Models.Requests;
using Orders.Services;
using Xunit;

namespace OrderTests.Services
{
  public class ScheduleServiceTests
  {
    private readonly Fixture _fixture = new Fixture();
    private readonly Mock<NullLogger<ScheduleService>> _logger =
      new Mock<NullLogger<ScheduleService>>();
    private readonly Mock<IValidator<ScheduleRequest>> _validator =
      new Mock<IValidator<ScheduleRequest>>();
    private readonly ScheduleService _service;

    public ScheduleServiceTests()
    {
      _service =
        new ScheduleService(
          _logger.Object, 
          _validator.Object);
    }

    [Fact]
    public async Task GenerateAsync_Fails_WhenValidationFails()
    {
      // arrange 
      var validationResult =
        new Mock<ValidationResult>();
      validationResult
        .SetupGet(_ => _.IsValid)
        .Returns(false);
      _validator
        .Setup(_ => _.ValidateAsync(It.IsAny<ScheduleRequest>(), new CancellationToken()))
        .ReturnsAsync(validationResult.Object);

      // act 
      var result =
        await _service.GenerateAsync(It.IsAny<ScheduleRequest>());

      // assert
      Assert.NotNull(result);
      Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
    }

    [Fact]
    public async Task GenerateAsync_Fails_WhenExceptionIsThrown()
    {
      // arrange 
      var requestObject = _fixture.Create<ScheduleRequest>();

      var validationResult =
        new Mock<ValidationResult>();
      validationResult
        .SetupGet(_ => _.IsValid)
        .Returns(true);
      _validator
        .Setup(_ => _.ValidateAsync(It.IsAny<ScheduleRequest>(), new CancellationToken()))
        .ThrowsAsync(new Exception());

      // act
      var result = await _service.GenerateAsync(requestObject);

      // assert
      Assert.NotNull(result);
      Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
    }
  }
}