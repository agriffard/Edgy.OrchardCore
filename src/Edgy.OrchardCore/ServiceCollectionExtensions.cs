namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Provides extensions methods for <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    public static void AddEdgyServices(this IServiceCollection services)
    {
        services.AddScoped<ContentService>(); //IContentService, 
    }
}
