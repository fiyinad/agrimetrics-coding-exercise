using Newtonsoft.Json;

namespace Orders.Models.Requests
{
  public class Order
  {
    [JsonProperty("customerName")]
    public string CustomerName { get; set; }
  }
}