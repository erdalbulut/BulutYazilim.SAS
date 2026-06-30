using BulutYazilim.SAS.Commons;
using BulutYazilim.SAS.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace BulutYazilim.SAS.Kasalar;

public class EfCoreKasaRepository : EfCoreCommonRepository<Kasa>, IKasaRepository
{
	public EfCoreKasaRepository(IDbContextProvider<SASDbContext> dbContextProvider)
		: base(dbContextProvider)
	{
	}
}