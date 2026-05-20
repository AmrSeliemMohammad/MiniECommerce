using MiniECommerce.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace MiniECommerce.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class MiniECommerceController : AbpControllerBase
{
    protected MiniECommerceController()
    {
        LocalizationResource = typeof(MiniECommerceResource);
    }
}
