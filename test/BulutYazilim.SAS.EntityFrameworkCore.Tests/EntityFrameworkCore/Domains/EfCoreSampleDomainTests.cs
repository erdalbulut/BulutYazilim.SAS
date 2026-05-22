using BulutYazilim.SAS.Samples;
using Xunit;

namespace BulutYazilim.SAS.EntityFrameworkCore.Domains;

[Collection(SASTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<SASEntityFrameworkCoreTestModule>
{

}
