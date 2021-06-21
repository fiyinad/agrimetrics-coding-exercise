using System.Threading.Tasks;
using Orders.Models.Requests;
using Orders.Models.Responses;
using Orders.Response;

namespace Orders.Services
{
  public interface IScheduleService
  {
    Task<IResponse<ScheduleResponse>> GenerateAsync(ScheduleRequest scheduleRequest);
  }
}