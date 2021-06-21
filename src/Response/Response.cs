using System.Net;

namespace Orders.Response
{
  /// <summary>
  /// Represents a custom http response. See <see cref="IResponse"/>
  /// </summary>
  public class Response : IResponse
  {
    /// <summary>
    /// Gets or sets the http response status code 
    /// </summary>
    /// <value>The http response status code</value>
    public HttpStatusCode StatusCode { get; set; }

    /// <summary>
    /// Gets or sets the response error message 
    /// </summary>
    /// <value>The http response error message</value>
    public string Error { get; set; }

    /// <summary>
    /// Creates an instance of <see cref="Response"/>
    /// </summary>
    /// <param name="statusCode">The response status code</param>
    public Response(HttpStatusCode statusCode)
    {
      StatusCode = statusCode;
    }

    /// <summary>
    /// Creates an instance of <see cref="Response"/>
    /// </summary>
    /// <param name="statusCode">response status code</param>
    /// <param name="error">error message</param>
    public Response(HttpStatusCode statusCode, string error)
    {
      StatusCode = statusCode;
      Error = error;
    }
  }

  /// <summary>
  /// Represents a custom generic http response. See <see cref="IResponse{T}"/>
  /// </summary>
  /// <typeparam name="T">the http response data type</typeparam>
  public class Response<T> : IResponse<T>
  {
    /// <summary>
    /// Gets or sets the response status code 
    /// </summary>
    /// <value>the response status code</value>
    public HttpStatusCode StatusCode { get; set; }

    /// <summary>
    /// Gets or sets the http response error message 
    /// </summary>
    /// <value>The http response error message</value>
    public string Error { get; set; }

    /// <summary>
    /// Gets or sets the http response data
    /// </summary>
    /// <value>The http response data</value>
    public T Data { get; set; }

    /// <summary>
    /// Creates an instance of <see cref="Response{T}" />
    /// </summary>
    /// <param name="statusCode">the response status code</param>
    public Response(HttpStatusCode statusCode)
    {
      StatusCode = statusCode;
    }

    /// <summary>
    /// Creates an instance of <see cref="Response{T}"/>
    /// </summary>
    /// <param name="statusCode">the http response status code</param>
    /// <param name="data">the http response data</param>
    public Response(HttpStatusCode statusCode, T data)
    {
      StatusCode = statusCode;
      Data = data;
    }

    /// <summary>
    /// Creates an instance of <see cref="Response{T}"/>
    /// </summary>
    /// <param name="statusCode">the http response statusCode</param>
    /// <param name="error">the http response error message</param>
    /// <param name="data">the http response data</param>
    public Response(HttpStatusCode statusCode, string error, T data)
    {
      StatusCode = statusCode;
      Error = error;
      Data = data;
    }
  }
}