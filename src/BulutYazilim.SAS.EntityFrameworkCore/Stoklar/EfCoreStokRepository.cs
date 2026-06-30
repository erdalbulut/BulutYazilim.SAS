using BulutYazilim.SAS.Commons;
using BulutYazilim.SAS.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;

namespace BulutYazilim.SAS.Stoklar;

public class EfCoreStokRepository : EfCoreCommonRepository<Stok>, IStokRepository
{
	public EfCoreStokRepository(IDbContextProvider<SASDbContext> dbContextProvider)
		: base(dbContextProvider)
	{
	}

	//public override async Task<IQueryable<Stok>> WithDetailsAsync()
	//{
	//	return (await GetQueryableAsync())
	//		.Include(x => x.Birim)
	//		.Include(x => x.OzelKod1)
	//		.Include(x => x.OzelKod2)
	//		.Include(x => x.FaturaHareketler).ThenInclude(x => x.Fatura);
	//}
}