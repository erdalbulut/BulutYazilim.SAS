using Volo.Abp.Modularity;

namespace BulutYazilim.SAS;

[DependsOn(
    typeof(SASApplicationModule),
    typeof(SASDomainTestModule)
)]
public class SASApplicationTestModule : AbpModule
{

}
