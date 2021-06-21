using System.Collections.Generic;
using Newtonsoft.Json;

namespace Orders.Models.Requests
{
  public class ScheduleRequest
  {
    [JsonProperty("orders")]
    public List<Order> Orders { get; set; }
  }
}