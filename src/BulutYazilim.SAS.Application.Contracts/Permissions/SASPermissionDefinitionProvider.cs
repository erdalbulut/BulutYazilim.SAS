using BulutYazilim.SAS.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace BulutYazilim.SAS.Permissions;

public class SASPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(SASPermissions.GroupName);

        //Define your own permissions here. Example:
        //myGroup.AddPermission(SASPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<SASResource>(name);
    }
}
