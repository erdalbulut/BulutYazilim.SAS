using BulutYazilim.SAS.Commons;
using BulutYazilim.SAS.EntityFrameworkCore;
using BulutYazilim.SAS.Makbuzlar;
using Volo.Abp.EntityFrameworkCore;

namespace BulutYazilim.SAS.MakbuzHareketler;

public class EfCoreMakbuzHareketRepository : EfCoreCommonRepository<MakbuzHareket>, IMakbuzHareketRepository
{
	public EfCoreMakbuzHareketRepository(IDbContextProvider<SASDbContext> dbContextProvider)
		: base(dbContextProvider)
	{
	}
}