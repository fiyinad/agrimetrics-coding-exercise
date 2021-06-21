using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Orders.Response
{
  /// <summary>
  /// Extends the custom type <see cref="IResponse"/>
  /// </summary>
  public static class ResponseExtensions
  {
    private static IList<HttpStatusCode> successStatusCodes = new List<HttpStatusCode>
        {
            HttpStatusCode.Accepted,
            HttpStatusCode.Created,
            HttpStatusCode.OK
        };

    /// <summary>
    /// Checks if http response has succeeded
    /// </summary>
    /// <param name="response">the http response</param>
    /// <returns>boolean</returns>
    public static bool HasSucceeded(this IResponse response)
    {
      return response != null &&
          successStatusCodes.Contains(response.StatusCode);
    }

    /// <summary>
    /// Checks if http response has succeeded
    /// </summary>
    /// <param name="response">the http response</param>
    /// <typeparam name="T">the http response data type</typeparam>
    /// <returns>boolean</returns>
    public static bool HasSucceeded<T>(this IResponse<T> response)
    {
      return response != null &&
          successStatusCodes.Contains(response.StatusCode);
    }

    /// <summary>
    /// Checks if http response has succeeded and has data
    /// </summary>
    /// <param name="response">the http response</param>
    /// <typeparam name="T">the http response data type</typeparam>
    /// <returns>boolean</returns>
    public static bool HasSucceededWithData<T>(this IResponse<T> response)
    {
      return response != null &&
          successStatusCodes.Contains(response.StatusCode) &&
          response.Data != null;
    }

    /// <summary>
    /// Gets result of http response
    /// </summary>
    /// <param name="response">The http response</param>
    /// <returns>The http responses status code result</returns>
    public static ActionResult GetResult(this IResponse response)
    {
      if (response == null)
      {
        return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
      }

      return new StatusCodeResult((int)response.StatusCode);
    }

    /// <summary>
    /// Gets result of custom response
    /// </summary>
    /// <param name="response">The http response</param>
    /// <typeparam name="T">the http response data type</typeparam>
    /// <returns>The http responses result</returns>
    public static ActionResult GetResult<T>(this IResponse<T> response)
    {
      if (response == null)
      {
        return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
      }

      if (response.HasSucceededWithData())
      {
        return new OkObjectResult(response.Data);
      }

      return new StatusCodeResult((int)response.StatusCode);
    }
  }
}