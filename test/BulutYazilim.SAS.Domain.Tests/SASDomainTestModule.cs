using Volo.Abp.Modularity;

namespace BulutYazilim.SAS;

[DependsOn(
    typeof(SASDomainModule),
    typeof(SASTestBaseModule)
)]
public class SASDomainTestModule : AbpModule
{

}
