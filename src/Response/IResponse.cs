using System.Net;

namespace Orders.Response
{
  /// <summary>
  /// Defines a common type for custom http responses 
  /// </summary>
  public interface IResponse
  {
    /// <summary>
    /// Gets or sets the http response status code 
    /// </summary>
    /// <value>The http response status code</value>
    HttpStatusCode StatusCode { get; set; }

    /// <summary>
    /// Gets or sets the http response error message 
    /// </summary>
    /// <value>The http response error message</value>
    string Error { get; set; }
  }

  /// <summary>
  /// Defines a common type for custom http responses 
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public interface IResponse<T>
  {
    /// <summary>
    /// Gets or sets the http response status code 
    /// </summary>
    /// <value>The response status code</value>
    HttpStatusCode StatusCode { get; set; }

    /// <summary>
    /// Gets or sets the http response error message 
    /// </summary>
    /// <value>The http response error message</value>
    string Error { get; set; }

    /// <summary>
    /// Gets or sets the http response data
    /// </summary>
    /// <value>The http response data</value>
    T Data { get; set; }
  }
}