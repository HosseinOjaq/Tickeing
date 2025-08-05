using System.Reflection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerUI;
using Ticketing.Api.Configurations.Swagger;

namespace Ticketing.Api.Extensions;

public static class SwaggerConfigurationExtensions
{
    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerExamplesFromAssemblies(Assembly.GetExecutingAssembly());

        //Add services and configuration to use swagger
        services.AddSwaggerGen(options =>
        {
            var xmlDocPath = Path.Combine(AppContext.BaseDirectory, "Ticketing.Api.xml");
            //show controller XML comments like summary
            options.IncludeXmlComments(xmlDocPath, true);
            options.EnableAnnotations();
            options.SwaggerDoc("v1", new OpenApiInfo { Version = "v1", Title = "API V1" });
            //options.SwaggerDoc("v2", new OpenApiInfo { Version = "v2", Title = "API V2" });

            //Enable to use [SwaggerRequestExample] & [SwaggerResponseExample]
            options.ExampleFilters();

            #region Add UnAuthorized to Response
            //Add 401 response and security requirements (Lock icon) to actions that need authorization
            options.OperationFilter<UnauthorizedResponsesOperationFilter>(true, "Bearer");
            #endregion

            #region Add Jwt Authentication            
            //OAuth2Scheme

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "توکن JWT را به صورت Bearer {token} وارد کنید",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });

            #endregion
        });
    }

    public static IApplicationBuilder UseSwaggerAndUI(this IApplicationBuilder app)
    {
        //Swagger middleware for generate "Open API Documentation" in swagger.json
        app.UseSwagger();

        //Swagger middleware for generate UI from swagger.json
        app.UseSwaggerUI(options =>
        {
            options.DisplayRequestDuration();
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "V1 Docs");
            options.DocExpansion(DocExpansion.None);
        });

        //ReDoc UI middleware. ReDoc UI is an alternative to swagger-ui
        app.UseReDoc(options =>
        {
            options.SpecUrl("/swagger/v1/swagger.json");

            options.EnableUntrustedSpec();
            options.ScrollYOffset(10);
            options.HideHostname();
            options.HideDownloadButton();
            options.ExpandResponses("200,201");
            options.RequiredPropsFirst();
            options.NoAutoAuth();
            options.PathInMiddlePanel();
            options.HideLoading();
            options.NativeScrollbars();
            options.DisableSearch();
            options.OnlyRequiredInSamples();
            options.SortPropsAlphabetically();
        });
        return app;
    }
}