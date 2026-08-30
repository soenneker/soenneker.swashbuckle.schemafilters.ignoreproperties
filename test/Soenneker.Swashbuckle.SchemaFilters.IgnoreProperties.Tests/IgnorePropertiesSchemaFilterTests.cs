using Microsoft.OpenApi;
using Soenneker.Swashbuckle.Attributes.IgnoreProperty;
using Soenneker.Tests.HostedUnit;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Threading.Tasks;

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

    [Test]
    public void Apply_should_ignore_schema_references()
    {
        var filter = new IgnorePropertiesSchemaFilter();
        var schema = new OpenApiSchemaReference("RequestDataOptions", new OpenApiDocument(), "3.0");
        var context = new SchemaFilterContext(typeof(string), null!, new SchemaRepository(), null, null);

        filter.Apply(schema, context);
    }

    [Test]
    public async Task Apply_should_remove_camel_case_schema_property()
    {
        var filter = new IgnorePropertiesSchemaFilter();
        var schema = new OpenApiSchema
        {
            Properties = new Dictionary<string, IOpenApiSchema> { ["internalValue"] = new OpenApiSchema() }
        };
        var context = new SchemaFilterContext(typeof(TestModel), null!, new SchemaRepository(), null, null);

        filter.Apply(schema, context);

        await Assert.That(schema.Properties).IsEmpty();
    }

    private sealed class TestModel
    {
        [OpenApiIgnoreProperty]
        public string? InternalValue { get; init; }
    }
}
