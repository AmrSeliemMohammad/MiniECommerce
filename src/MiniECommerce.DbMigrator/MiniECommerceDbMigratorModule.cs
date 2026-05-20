using MiniECommerce.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace MiniECommerce.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(MiniECommerceEntityFrameworkCoreModule),
    typeof(MiniECommerceApplicationContractsModule)
)]
public class MiniECommerceDbMigratorModule : AbpModule
{
}
