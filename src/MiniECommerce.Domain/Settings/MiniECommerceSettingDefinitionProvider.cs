using Volo.Abp.Settings;

namespace MiniECommerce.Settings;

public class MiniECommerceSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(MiniECommerceSettings.MySetting1));
    }
}
