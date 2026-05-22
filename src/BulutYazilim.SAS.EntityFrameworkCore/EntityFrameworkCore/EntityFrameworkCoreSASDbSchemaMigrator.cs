using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BulutYazilim.SAS.Data;
using Volo.Abp.DependencyInjection;

namespace BulutYazilim.SAS.EntityFrameworkCore;

public class EntityFrameworkCoreSASDbSchemaMigrator
    : ISASDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreSASDbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the SASDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<SASDbContext>()
            .Database
            .MigrateAsync();
    }
}
