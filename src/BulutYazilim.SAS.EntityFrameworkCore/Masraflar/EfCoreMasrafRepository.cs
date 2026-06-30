using BulutYazilim.SAS.Commons;
using BulutYazilim.SAS.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;

namespace BulutYazilim.SAS.Masraflar;

public class EfCoreMasrafRepository : EfCoreCommonRepository<Masraf>, IMasrafRepository
{
	public EfCoreMasrafRepository(IDbContextProvider<SASDbContext> dbContextProvider)
		: base(dbContextProvider)
	{
	}

	//public override async Task<IQueryable<Masraf>> WithDetailsAsync()
	//{
	//	return (await GetQueryableAsync())
	//		.Include(x => x.Birim)
	//		.Include(x => x.OzelKod1)
	//		.Include(x => x.OzelKod2)
	//		.Include(x => x.FaturaHareketler).ThenInclude(x => x.Fatura);
	//}
}