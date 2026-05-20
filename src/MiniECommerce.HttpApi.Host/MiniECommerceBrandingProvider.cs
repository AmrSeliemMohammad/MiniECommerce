using Microsoft.Extensions.Localization;
using MiniECommerce.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace MiniECommerce;

[Dependency(ReplaceServices = true)]
public class MiniECommerceBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<MiniECommerceResource> _localizer;

    public MiniECommerceBrandingProvider(IStringLocalizer<MiniECommerceResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
