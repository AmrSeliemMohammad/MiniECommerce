using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace MiniECommerce.Data;

/* This is used if database provider does't define
 * IMiniECommerceDbSchemaMigrator implementation.
 */
public class NullMiniECommerceDbSchemaMigrator : IMiniECommerceDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
