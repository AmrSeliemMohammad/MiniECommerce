using Volo.Abp.Modularity;

namespace MiniECommerce;

[DependsOn(
    typeof(MiniECommerceDomainModule),
    typeof(MiniECommerceTestBaseModule)
)]
public class MiniECommerceDomainTestModule : AbpModule
{

}
