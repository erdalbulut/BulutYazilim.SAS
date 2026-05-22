using System.Threading.Tasks;

namespace BulutYazilim.SAS.Data;

public interface ISASDbSchemaMigrator
{
    Task MigrateAsync();
}
