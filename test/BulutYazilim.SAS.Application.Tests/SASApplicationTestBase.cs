using Volo.Abp.Modularity;

namespace BulutYazilim.SAS;

public abstract class SASApplicationTestBase<TStartupModule> : SASTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
