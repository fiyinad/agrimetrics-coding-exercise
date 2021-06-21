using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Orders.Extensions;
using Orders.HealthCheck;
using Orders.Models.Requests;
using Orders.Services;
using Orders.Swagger;
using Orders.Validators;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Orders
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddRouting(options => options.LowercaseUrls = true);

      services.AddControllers();
      services.AddVersionedApiExplorer(c =>
      {
        c.GroupNameFormat = "'v'V";
        c.SubstituteApiVersionInUrl = true;
      });
      services.AddSwaggerGen();
      services.AddApiVersioning();
      services.AddTransient<ISwaggerProvider, SwaggerGenerator>();
      services.AddTransient<ISchemaGenerator, SchemaGenerator>();
      services.AddTransient<IConfigureOptions<SwaggerGenOptions>, MySwaggerGenOptions>();

      // validators
      services.AddTransient<IValidator<ScheduleRequest>, ScheduleRequestValidator>();

      // mapper(s)
      services.AddAutoMapper(Assembly.GetExecutingAssembly());

      // service(s)
      services.AddTransient<IScheduleService, ScheduleService>();

      // health checks
      services.AddHealthChecks()
        .AddCheck("self", () => HealthCheckResult.Healthy(), tags: new[] { "self" });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(
      IApplicationBuilder app, 
      IWebHostEnvironment env,
      IApiVersionDescriptionProvider provider)
    {
      if (env.IsLocal() || env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseRouting();
      app.UseApiVersioning();
      app.UseSwagger();
      app.UseSwaggerUI(c =>
      {
        foreach (var description in provider.ApiVersionDescriptions)
        {
          c.SwaggerEndpoint(
              $"/swagger/{description.GroupName}/swagger.json",
              $"{description.GroupName.ToUpperInvariant()}");
        }
      });

      app.UseHealthChecks("/healthz/self", HealthCheckHelpers.FormattedOptions(new[] { "self" }));

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
