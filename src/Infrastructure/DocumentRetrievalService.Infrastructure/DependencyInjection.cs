using DocumentRetrievalService.Application.Common.Interfaces;
using DocumentRetrievalService.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DocumentRetrievalService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<ICsvExporter, CsvExporter>();

        return services;
    }
}
