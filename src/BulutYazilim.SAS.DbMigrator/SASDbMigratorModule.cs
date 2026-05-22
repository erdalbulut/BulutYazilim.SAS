using BulutYazilim.SAS.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace BulutYazilim.SAS.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(SASEntityFrameworkCoreModule),
    typeof(SASApplicationContractsModule)
)]
public class SASDbMigratorModule : AbpModule
{
}
