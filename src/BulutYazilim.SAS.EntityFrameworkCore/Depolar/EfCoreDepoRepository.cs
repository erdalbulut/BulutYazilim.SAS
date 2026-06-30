using BulutYazilim.SAS.Commons;
using BulutYazilim.SAS.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;

namespace BulutYazilim.SAS.Depolar;

public class EfCoreDepoRepository : EfCoreCommonRepository<Depo>, IDepoRepository
{
	public EfCoreDepoRepository(IDbContextProvider<SASDbContext> dbContextProvider)
		: base(dbContextProvider)
	{
	}

	//public override async Task<IQueryable<Depo>> WithDetailsAsync()
	//{
	//	return (await GetQueryableAsync())
	//		.Include(x => x.OzelKod1)
	//		.Include(x => x.OzelKod2)
	//		.Include(x => x.FaturaHareketler).ThenInclude(x => x.Fatura);
	//}
}