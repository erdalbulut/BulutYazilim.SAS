using BulutYazilim.SAS.Localization;
using Volo.Abp.AspNetCore.Components;

namespace BulutYazilim.SAS.Blazor;

public abstract class SASComponentBase : AbpComponentBase
{
    protected SASComponentBase()
    {
        LocalizationResource = typeof(SASResource);
    }
}
