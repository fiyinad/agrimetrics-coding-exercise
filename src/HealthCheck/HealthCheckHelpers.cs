using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Newtonsoft.Json;

namespace Orders.HealthCheck
{
  /// <summary>
  /// Helper class for HealthCheck middleware
  /// </summary>
  public static class HealthCheckHelpers
  {
    /// <summary>
    /// Formats HealthCheck JSON response by filtering out health checks using specified tag(s)
    /// </summary>
    /// <param name="tags"></param>
    /// <returns></returns>
    public static HealthCheckOptions FormattedOptions(string[] tags = null)
    {

      return new HealthCheckOptions
      {
        Predicate = (check) => tags == null || check.Tags.Intersect(tags).Any(),
        ResponseWriter = async (context, report) =>
        {
          context.Response.ContentType = "application/json; charset=utf-8";
          var bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(report));
          await context.Response.Body.WriteAsync(bytes);
        }
      };
    }
  }
}