[![](https://img.shields.io/nuget/v/soenneker.swashbuckle.schemafilters.ignoreproperties.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.swashbuckle.schemafilters.ignoreproperties/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.swashbuckle.schemafilters.ignoreproperties/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.swashbuckle.schemafilters.ignoreproperties/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.swashbuckle.schemafilters.ignoreproperties.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.swashbuckle.schemafilters.ignoreproperties/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.swashbuckle.schemafilters.ignoreproperties/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.swashbuckle.schemafilters.ignoreproperties/actions/workflows/codeql.yml)

# Soenneker.Swashbuckle.SchemaFilters.IgnoreProperties

A schema filter that removes properties from Swagger/OpenAPI documentation if they are marked with the `OpenApiIgnoreProperty`.

## Install

```bash
dotnet add package Soenneker.Swashbuckle.SchemaFilters.IgnoreProperties
```

## What you get

- `IgnorePropertiesSchemaFilter` — A schema filter that removes properties from Swagger/OpenAPI documentation if they are marked with the `OpenApiIgnoreProperty`.

## API at a glance

| API | What it does | Result / important behavior |
| --- | --- | --- |
| `IgnorePropertiesSchemaFilter.Apply(schema, context)` | Applies the filter by removing properties from the generated OpenAPI schema that have the `OpenApiIgnoreProperty`. | Returns no value; the requested change is complete when the method returns. |

## Important behavior

- `IgnorePropertiesSchemaFilter`: This only affects schema generation for Swagger and has no impact on runtime serialization.
