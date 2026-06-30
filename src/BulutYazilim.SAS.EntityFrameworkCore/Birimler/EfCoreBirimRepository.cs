using BulutYazilim.SAS.Commons;
using BulutYazilim.SAS.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace BulutYazilim.SAS.Birimler;

public class EfCoreBirimRepository : EfCoreCommonRepository<Birim>, IBirimRepository
{
	public EfCoreBirimRepository(IDbContextProvider<SASDbContext> dbContextProvider) 
		: base(dbContextProvider)
	{
	}
}