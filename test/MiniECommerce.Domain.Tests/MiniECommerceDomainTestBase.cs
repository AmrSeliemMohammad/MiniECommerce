using Volo.Abp.Modularity;

namespace MiniECommerce;

/* Inherit from this class for your domain layer tests. */
public abstract class MiniECommerceDomainTestBase<TStartupModule> : MiniECommerceTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
