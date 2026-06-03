using MiniECommerce.Samples;
using Xunit;

namespace MiniECommerce.EntityFrameworkCore.Domains;

[Collection(MiniECommerceTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<MiniECommerceEntityFrameworkCoreTestModule>
{

}
