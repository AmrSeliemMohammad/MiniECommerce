using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MiniECommerce.Data;
using Volo.Abp.DependencyInjection;

namespace MiniECommerce.EntityFrameworkCore;

public class EntityFrameworkCoreMiniECommerceDbSchemaMigrator
    : IMiniECommerceDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreMiniECommerceDbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the MiniECommerceDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<MiniECommerceDbContext>()
            .Database
            .MigrateAsync();
    }
}
