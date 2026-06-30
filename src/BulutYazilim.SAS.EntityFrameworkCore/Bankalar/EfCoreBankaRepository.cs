using BulutYazilim.SAS.Commons;
using BulutYazilim.SAS.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace BulutYazilim.SAS.Bankalar;

public class EfCoreBankaRepository : EfCoreCommonRepository<Banka>, IBankaRepository
{
	public EfCoreBankaRepository(IDbContextProvider<SASDbContext> dbContextProvider)
		: base(dbContextProvider)
	{
	}
}