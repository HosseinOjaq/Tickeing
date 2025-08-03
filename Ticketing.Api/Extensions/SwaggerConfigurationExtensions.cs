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

            //Set summary of action if not already set
            options.OperationFilter<ApplySummariesOperationFilter>();

            #region Add UnAuthorized to Response
            //Add 401 response and security requirements (Lock icon) to actions that need authorization
            options.OperationFilter<UnauthorizedResponsesOperationFilter>(true, "OAuth2");
            #endregion

            #region Add Jwt Authentication            
            //OAuth2Scheme
            options.AddSecurityDefinition("OAuth2", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    Password = new OpenApiOAuthFlow
                    {
                        TokenUrl = new Uri("/api/v1/account/sign-in", UriKind.Relative)
                    }
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