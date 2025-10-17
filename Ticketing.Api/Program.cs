using Ticketing.Application;
using Ticketing.Api.Extensions;
using Ticketing.Infrastructure;
using Ticketing.Api.Middlewares;
using Ticketing.Domain.Models.Configurations;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        Stimulsoft.Base.StiLicense.Key = "";

        builder.Services.AddCustomCors();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.RegisterApplicationServices()
                        .RegisterInfrastructureServices(builder.Configuration);

        builder.Services.AddSwagger();

        var siteSettingsConfiguration = builder.Configuration.GetSection(nameof(SiteSettings));
        var siteSettings = siteSettingsConfiguration.Get<SiteSettings>();

        builder.Services.AddSingleton(siteSettings!);

        builder.Services.AddJwtAuthentication(siteSettings!.JwtSettings);

        var app = builder.Build();

        app.UseStaticFiles();

        app.UseCors("CustomCors");
        app.UseCustomExceptionHandler();
        app.UseSwaggerAndUI();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        await app.RunAsync();
    }
}