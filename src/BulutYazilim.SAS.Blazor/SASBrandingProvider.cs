using Microsoft.Extensions.Localization;
using BulutYazilim.SAS.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace BulutYazilim.SAS.Blazor;

[Dependency(ReplaceServices = true)]
public class SASBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<SASResource> _localizer;

    public SASBrandingProvider(IStringLocalizer<SASResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
