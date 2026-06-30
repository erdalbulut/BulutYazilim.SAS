using BulutYazilim.SAS.Commons;
using BulutYazilim.SAS.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.EntityFrameworkCore;

namespace BulutYazilim.SAS.BankaHesaplar;

public class EfCoreBankaHesapRepository : EfCoreCommonRepository<BankaHesap>, IBankaHesapRepository
{
	public EfCoreBankaHesapRepository(IDbContextProvider<SASDbContext> dbContextProvider)
		: base(dbContextProvider)
	{
	}
}