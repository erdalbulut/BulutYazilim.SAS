using BulutYazilim.SAS.Samples;
using Xunit;

namespace BulutYazilim.SAS.EntityFrameworkCore.Applications;

[Collection(SASTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<SASEntityFrameworkCoreTestModule>
{

}
