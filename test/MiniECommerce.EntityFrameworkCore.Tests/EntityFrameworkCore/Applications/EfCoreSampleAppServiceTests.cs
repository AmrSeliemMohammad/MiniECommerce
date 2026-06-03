using MiniECommerce.Samples;
using Xunit;

namespace MiniECommerce.EntityFrameworkCore.Applications;

[Collection(MiniECommerceTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<MiniECommerceEntityFrameworkCoreTestModule>
{

}
