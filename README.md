[![](https://img.shields.io/nuget/v/soenneker.swashbuckle.schemafilters.ignoreproperties.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.swashbuckle.schemafilters.ignoreproperties/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.swashbuckle.schemafilters.ignoreproperties/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.swashbuckle.schemafilters.ignoreproperties/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.swashbuckle.schemafilters.ignoreproperties.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.swashbuckle.schemafilters.ignoreproperties/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.swashbuckle.schemafilters.ignoreproperties/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.swashbuckle.schemafilters.ignoreproperties/actions/workflows/codeql.yml)

# Soenneker.Swashbuckle.SchemaFilters.IgnoreProperties

Removes properties marked with `OpenApiIgnoreProperty` from Swashbuckle-generated schemas.

## Installation

```bash
dotnet add package Soenneker.Swashbuckle.SchemaFilters.IgnoreProperties
```

## Registration

```csharp
using Soenneker.Swashbuckle.SchemaFilters.IgnoreProperties;

builder.Services.AddSwaggerGen(options =>
{
    options.SchemaFilter<IgnorePropertiesSchemaFilter>();
});
```

The package includes the marker attribute dependency:

```csharp
using Soenneker.Swashbuckle.Attributes.IgnoreProperty;

public sealed class UserResponse
{
    public required string DisplayName { get; init; }

    [OpenApiIgnoreProperty]
    public string? InternalCorrelationId { get; init; }
}
```

`InternalCorrelationId` is omitted from the generated schema. The filter honors property names explicitly set with System.Text.Json's `JsonPropertyName` or Newtonsoft.Json's `JsonProperty`, and it handles the common PascalCase-to-camelCase naming difference.

Custom naming policies that change more than casing should pair the property with an explicit JSON-name attribute so the filter can identify the generated schema key.

This is documentation filtering, not data protection. The property remains available to model binding and JSON serialization unless the corresponding serializer-ignore attribute is also applied.
