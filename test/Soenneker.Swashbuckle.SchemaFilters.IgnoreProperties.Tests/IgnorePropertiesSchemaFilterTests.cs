using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.Swashbuckle.SchemaFilters.IgnoreProperties.Tests;

[Collection("Collection")]
public class IgnorePropertiesSchemaFilterTests : FixturedUnitTest
{
    public IgnorePropertiesSchemaFilterTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
    }

    [Fact]
    public void Default()
    {
    }
}