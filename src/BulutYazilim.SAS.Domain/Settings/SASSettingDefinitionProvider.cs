using Volo.Abp.Settings;

namespace BulutYazilim.SAS.Settings;

public class SASSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(SASSettings.MySetting1));
    }
}
