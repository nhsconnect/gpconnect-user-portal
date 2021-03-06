using GpConnect.NationalDataSharingPortal.EndUserPortal.Core;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Logging;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal;

public class Startup
{
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
    {
        if (configuration == null) throw new ArgumentNullException(nameof(configuration));

        _configuration = configuration;
        _webHostEnvironment = webHostEnvironment;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddOptions();
        services.AddHttpContextAccessor();
        services.ConfigureApplicationServices(_configuration, _webHostEnvironment);
        services.ConfigureLoggingServices(_configuration);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.ConfigureApplicationBuilderServices(env);
    }
}
