using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Net.Http.Headers;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Core;

public static class ApplicationBuilderExtensions
{
  public static void ConfigureApplicationBuilderServices(this IApplicationBuilder app)
  {
    app.UseDeveloperExceptionPage();

    app.UseForwardedHeaders(new ForwardedHeadersOptions
    {
      ForwardedHeaders = ForwardedHeaders.XForwardedProto
    });

    app.UseHsts();

    app.UseStatusCodePagesWithReExecute("/Error/{0}");

    app.UseHttpsRedirection();

    app.UseStaticFiles(new StaticFileOptions
    {
      OnPrepareResponse = context =>
      {
        context.Context.Response.Headers[HeaderNames.CacheControl] = $"public, max-age={TimeSpan.FromSeconds(60 * 60 * 24)}";
      }
    });
    app.UseSession();
    app.UseCookiePolicy();
    app.UseRouting();
    app.UseResponseCaching();

    app.Use(async (context, next) =>
    {
      context.Response.GetTypedHeaders().CacheControl = new CacheControlHeaderValue()
      {
        NoStore = true,
        NoCache = true
      };
      context.Response.Headers.Add("Pragma", "no-cache");
      await next();
    });

    app.UseResponseCompression();

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseEndpoints(endpoints =>
    {
      endpoints.MapRazorPages();
      endpoints.MapControllers();
      endpoints.MapHealthChecks("/healthcheck", new HealthCheckOptions()
      {
        ResultStatusCodes =
                  {
                        [HealthStatus.Healthy] = StatusCodes.Status200OK,
                        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
                  },
        AllowCachingResponses = false
      });
    });
  }
}
