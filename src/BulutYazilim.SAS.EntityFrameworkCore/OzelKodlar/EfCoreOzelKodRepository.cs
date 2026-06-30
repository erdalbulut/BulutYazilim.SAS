using BulutYazilim.SAS.Commons;
using BulutYazilim.SAS.EntityFrameworkCore;
using BulutYazilim.SAS.Ozelkodlar;
using Volo.Abp.EntityFrameworkCore;

namespace BulutYazilim.SAS.OzelKodlar;

public class EfCoreOzelKodRepository : EfCoreCommonRepository<OzelKod>, IOzelKodRepository
{
	public EfCoreOzelKodRepository(IDbContextProvider<SASDbContext> dbContextProvider)
		: base(dbContextProvider)
	{
	}
}