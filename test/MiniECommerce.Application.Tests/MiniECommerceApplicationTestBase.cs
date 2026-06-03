using Volo.Abp.Modularity;

namespace MiniECommerce;

public abstract class MiniECommerceApplicationTestBase<TStartupModule> : MiniECommerceTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
