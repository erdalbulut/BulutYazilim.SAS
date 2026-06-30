using BulutYazilim.SAS.Commons;
using BulutYazilim.SAS.EntityFrameworkCore;
using BulutYazilim.SAS.Faturalar;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.EntityFrameworkCore;

namespace BulutYazilim.SAS.FaturaHareketler;

public class EfCoreFaturaHareketRepository : EfCoreCommonRepository<FaturaHareket>,
	IFaturaHareketRepository
{
	public EfCoreFaturaHareketRepository(IDbContextProvider<SASDbContext> dbContextProvider)
		: base(dbContextProvider)
	{
	}
}