using MiniECommerce.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace MiniECommerce.Permissions;

public class MiniECommercePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(MiniECommercePermissions.GroupName);

        //Define your own permissions here. Example:
        //myGroup.AddPermission(MiniECommercePermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<MiniECommerceResource>(name);
    }
}
