using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace BulutYazilim.SAS.Data;

/* This is used if database provider does't define
 * ISASDbSchemaMigrator implementation.
 */
public class NullSASDbSchemaMigrator : ISASDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
