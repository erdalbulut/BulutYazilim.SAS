using BulutYazilim.SAS.Commons;
using BulutYazilim.SAS.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.EntityFrameworkCore;

namespace BulutYazilim.SAS.Cariler;

public class EfCoreCariRepository : EfCoreCommonRepository<Cari>, ICariRepository
{
	public EfCoreCariRepository(IDbContextProvider<SASDbContext> dbContextProvider) 
		: base(dbContextProvider)
	{
	}
}