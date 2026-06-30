using BulutYazilim.SAS.Commons;
using BulutYazilim.SAS.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace BulutYazilim.SAS.Parametreler;

public class EfCoreFirmaParametreRepository : EfCoreCommonRepository<FirmaParametre>, IFirmaParametreRepository
{
	public EfCoreFirmaParametreRepository(IDbContextProvider<SASDbContext> dbContextProvider)
		: base(dbContextProvider)
	{
	}
}