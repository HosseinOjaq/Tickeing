namespace Ticketing.Api.Middlewares;

public static class ApplicationBuilderConfiguration
{
    public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
    }
}