using Soenneker.Tests.HostedUnit;

namespace Soenneker.Swashbuckle.SchemaFilters.IgnoreProperties.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public class IgnorePropertiesSchemaFilterTests : HostedUnitTest
{
    public IgnorePropertiesSchemaFilterTests(Host host) : base(host)
    {
    }

    [Test]
    public void Default()
    {
    }
}