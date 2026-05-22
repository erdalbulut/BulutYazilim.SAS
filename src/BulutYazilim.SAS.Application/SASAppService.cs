using BulutYazilim.SAS.Localization;
using Volo.Abp.Application.Services;

namespace BulutYazilim.SAS;

/* Inherit your application services from this class.
 */
public abstract class SASAppService : ApplicationService
{
    protected SASAppService()
    {
        LocalizationResource = typeof(SASResource);
    }
}
