using Volo.Abp.Modularity;

namespace BulutYazilim.SAS;

/* Inherit from this class for your domain layer tests. */
public abstract class SASDomainTestBase<TStartupModule> : SASTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
