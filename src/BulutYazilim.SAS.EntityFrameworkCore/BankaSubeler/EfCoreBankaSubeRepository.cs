using BulutYazilim.SAS.Commons;
using BulutYazilim.SAS.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.EntityFrameworkCore;

namespace BulutYazilim.SAS.BankaSubeler;

public class EfCoreBankaSubeRepository : EfCoreCommonRepository<BankaSube>, IBankaSubeRepository
{
	public EfCoreBankaSubeRepository(IDbContextProvider<SASDbContext> dbContextProvider) 
		: base(dbContextProvider)
	{
	}
}