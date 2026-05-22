using BulutYazilim.SAS.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace BulutYazilim.SAS.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class SASController : AbpControllerBase
{
    protected SASController()
    {
        LocalizationResource = typeof(SASResource);
    }
}
