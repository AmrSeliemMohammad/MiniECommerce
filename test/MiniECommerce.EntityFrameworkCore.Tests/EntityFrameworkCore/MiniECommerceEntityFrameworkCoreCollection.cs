using Xunit;

namespace MiniECommerce.EntityFrameworkCore;

[CollectionDefinition(MiniECommerceTestConsts.CollectionDefinitionName)]
public class MiniECommerceEntityFrameworkCoreCollection : ICollectionFixture<MiniECommerceEntityFrameworkCoreFixture>
{

}
