using System.Threading.Tasks;

namespace MiniECommerce.Data;

public interface IMiniECommerceDbSchemaMigrator
{
    Task MigrateAsync();
}
