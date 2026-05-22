using Xunit;

namespace BulutYazilim.SAS.EntityFrameworkCore;

[CollectionDefinition(SASTestConsts.CollectionDefinitionName)]
public class SASEntityFrameworkCoreCollection : ICollectionFixture<SASEntityFrameworkCoreFixture>
{

}
