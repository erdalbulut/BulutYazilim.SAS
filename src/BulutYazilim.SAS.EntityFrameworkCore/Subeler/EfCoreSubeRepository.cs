using BulutYazilim.SAS.Commons;
using BulutYazilim.SAS.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace BulutYazilim.SAS.Subeler;

public class EfCoreSubeRepository : EfCoreCommonRepository<Sube>, ISubeRepository
{
	public EfCoreSubeRepository(IDbContextProvider<SASDbContext> dbContextProvider)
		: base(dbContextProvider)
	{
	}
}