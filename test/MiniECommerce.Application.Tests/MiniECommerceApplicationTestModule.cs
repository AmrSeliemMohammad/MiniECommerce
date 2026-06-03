using Volo.Abp.Modularity;

namespace MiniECommerce;

[DependsOn(
    typeof(MiniECommerceApplicationModule),
    typeof(MiniECommerceDomainTestModule)
)]
public class MiniECommerceApplicationTestModule : AbpModule
{

}
