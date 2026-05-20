using MiniECommerce.Localization;
using Volo.Abp.Application.Services;

namespace MiniECommerce;

/* Inherit your application services from this class.
 */
public abstract class MiniECommerceAppService : ApplicationService
{
    protected MiniECommerceAppService()
    {
        LocalizationResource = typeof(MiniECommerceResource);
    }
}
