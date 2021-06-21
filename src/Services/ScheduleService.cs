using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Orders.Models.Requests;
using Orders.Models.Responses;
using Orders.Response;

namespace Orders.Services
{
  public class ScheduleService : BaseService, IScheduleService
  {
    private readonly ILogger<ScheduleService> _logger;
    private readonly IValidator<ScheduleRequest> _scheduleRequestValidator;
    public ScheduleService(
      ILogger<ScheduleService> logger,
      IValidator<ScheduleRequest> scheduleRequestValidator) : base(logger)
    {
      _logger = logger;
      _scheduleRequestValidator = scheduleRequestValidator;
    }

    public async Task<IResponse<ScheduleResponse>> GenerateAsync(ScheduleRequest request)
    {
      try
      {
        // define local variables 
        TimeSpan start = TimeSpan.FromMinutes(0);
        TimeSpan current = TimeSpan.FromMinutes(0);

        // validate
        var validationResult =
          await _scheduleRequestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
          string errorMessage = string.Join(Environment.NewLine, validationResult.Errors);
          return BadRequest<ScheduleResponse>(errorMessage);
        }

        ScheduleResponse result = new ScheduleResponse()
        {
          Tasks = new List<string>()
        };
        foreach (var order in request.Orders)
        {
          for (int step = 0; step < 2; step++)
          {
            var taskStr = string.Empty;
            var time = current.ToString("hh\\:mm");
            if (step == 0) // Make sandwich
            {
              taskStr = $"{time}   Make sandwich for {order.CustomerName}";
              current = current.Add(TimeSpan.FromSeconds(150));
            }
            else // Serve sandwich
            {
              taskStr = $"{time}   Serve sandwich for {order.CustomerName}";
              current = current.Add(TimeSpan.FromSeconds(60));
            }
            result.Tasks.Add(taskStr);
          }
        }

        if (result.Tasks.Count > 0)
        {
          result.Tasks.Add($"{current.ToString("hh\\:mm")}   Take a break");
        }
        return Success(result);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, ex.Message);
        return Failure<ScheduleResponse>(ex.Message);
      }
    }
  }
}