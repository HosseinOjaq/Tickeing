using Ticketing.Api.Models;
using Ticketing.Application;
using Ticketing.Api.Extensions;
using Ticketing.Infrastructure;
using Ticketing.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.RegisterApplicationServices()
                .RegisterInfrastructureServices(builder.Configuration);

builder.Services.AddSwagger();

var siteSettingsConfiguration = builder.Configuration.GetSection(nameof(SiteSettings));
var siteSettings = siteSettingsConfiguration.Get<SiteSettings>();

builder.Services.AddJwtAuthentication(siteSettings!.JwtSettings);

var app = builder.Build();
app.UseCustomExceptionHandler();
app.UseSwaggerAndUI();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();