using System.Net;
using Orders.Response;
using Microsoft.Extensions.Logging;

namespace Orders.Services
{
  /// <summary>
  /// Base class for all custom http request related actions
  /// </summary>
  public class BaseService
  {
    /// <summary>
    /// Gets or sets the logger
    /// </summary>
    /// <value></value>
    protected ILogger Logger { get; set; }

    /// <summary>
    /// Creates instance of <see cref="BaseService"/>
    /// </summary>
    /// <param name="logger">the logger</param>
    public BaseService(ILogger logger)
    {
      Logger = logger;
    }

    /// <summary>
    /// Returns a successful http response 
    /// </summary>
    /// <returns>IResponse</returns>
    protected IResponse Success() => new Response.Response(HttpStatusCode.OK);

    /// <summary>
    /// Returns a successful http response with data
    /// </summary>
    /// <param name="data">the http response data</param>
    /// <typeparam name="T">the http response data type</typeparam>
    /// <returns>IResponse</returns>
    protected IResponse<T> Success<T>(T data) => new Response<T>(HttpStatusCode.OK, data);

    /// <summary>
    /// Returns a created http response
    /// </summary>
    /// <returns>IResponse</returns>
    protected IResponse Created() => new Response.Response(HttpStatusCode.Created);

    /// <summary>
    /// Returns a bad request http response
    /// </summary>
    /// <param name="message">the http response message</param>
    /// <returns>Bad request response</returns>
    protected IResponse BadRequest(string message)
    {
      Logger.LogError(message);
      return new Response.Response(HttpStatusCode.BadRequest, message);
    }

    /// <summary>
    /// Returns a bad request http response with error message
    /// </summary>
    /// <param name="message">the http response error message</param>
    /// <typeparam name="T">the http response data type</typeparam>
    /// <returns>IResponse</returns>
    protected IResponse<T> BadRequest<T>(string message)
    {
      Logger.LogError(message);
      return new Response<T>(HttpStatusCode.BadRequest, message, default(T));
    }

    /// <summary>
    /// Returns a failed http response with error message
    /// </summary>
    /// <param name="message">the http response message</param>
    /// <returns>IResponse</returns>
    protected IResponse Failure(string message)
    {
      Logger.LogError(message);
      return new Response.Response(HttpStatusCode.InternalServerError, message);
    }

    /// <summary>
    /// Returns a failed http response with error message and data
    /// </summary>
    /// <param name="message">the http response error message</param>
    /// <typeparam name="T">the http response data type</typeparam>
    /// <returns>IResponse</returns>
    protected IResponse<T> Failure<T>(string message)
    {
      Logger.LogError(message);
      return new Response<T>(HttpStatusCode.InternalServerError, message, default(T));
    }

    /// <summary>
    /// Returns a unauthorized http response
    /// </summary>
    /// <returns>IResponse</returns>
    protected IResponse Unauthorized() => new Response.Response(HttpStatusCode.Unauthorized);

    /// <summary>
    /// Returns an unauthorized http response
    /// </summary>
    /// <typeparam name="T">the http response data type</typeparam>
    /// <returns></returns>
    protected IResponse<T> Unauthorized<T>() => new Response<T>(HttpStatusCode.Unauthorized, default(T));
  }
}