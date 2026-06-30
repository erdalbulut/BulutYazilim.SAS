using BulutYazilim.SAS.Commons;
using BulutYazilim.SAS.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace BulutYazilim.SAS.Donemler;

public class EfCoreDonemRepository : EfCoreCommonRepository<Donem>, IDonemRepository
{
	public EfCoreDonemRepository(IDbContextProvider<SASDbContext> dbContextProvider)
		: base(dbContextProvider)
	{
	}
}