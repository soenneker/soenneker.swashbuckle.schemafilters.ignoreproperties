using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.OpenApi;
using Soenneker.Swashbuckle.Attributes.IgnoreProperty;
using System;
using System.Linq;

namespace Soenneker.Swashbuckle.SchemaFilters.IgnoreProperties;

/// <summary>
/// A schema filter that removes properties from Swagger/OpenAPI documentation 
/// if they are marked with the <see cref="OpenApiIgnoreProperty"/>.
/// </summary>
/// <remarks>
/// This only affects schema generation for Swagger and has no impact on runtime serialization.
/// </remarks>
public sealed class IgnorePropertiesSchemaFilter : ISchemaFilter
{
    private readonly Func<Type, IEnumerable<string>> _ignoredNames;

    /// <summary>Discovers ignored properties at runtime.</summary>
    [RequiresUnreferencedCode("Runtime schema discovery requires preserved properties. Supply explicit ignored JSON names when trimming.")]
    public IgnorePropertiesSchemaFilter() => _ignoredNames = Discover;

    /// <summary>Uses an explicit type-to-ignored-JSON-names map without reflection.</summary>
    public IgnorePropertiesSchemaFilter(IReadOnlyDictionary<Type, IReadOnlyList<string>> ignoredNames)
    {
        ArgumentNullException.ThrowIfNull(ignoredNames);
        var snapshot = ignoredNames.ToDictionary(pair => pair.Key, pair => pair.Value.ToArray());
        _ignoredNames = type => snapshot.TryGetValue(type, out var names) ? names : Array.Empty<string>();
    }

    [RequiresUnreferencedCode("Runtime schema discovery requires preserved properties.")]
    private static IEnumerable<string> Discover(Type type) => type.GetProperties()
        .Where(property => property.GetCustomAttribute<OpenApiIgnoreProperty>() is not null)
        .Select(property => property.GetCustomAttribute<JsonPropertyNameAttribute>()?.Name ?? property.Name);

    /// <summary>
    /// Applies the filter by removing properties from the generated OpenAPI schema
    /// that have the <see cref="OpenApiIgnoreProperty"/>.
    /// </summary>
    /// <param name="schema">The OpenAPI schema being generated.</param>
    /// <param name="context">The context for schema generation, including the target type.</param>
    public void Apply(IOpenApiSchema schema, SchemaFilterContext context)
    {
        if (schema is not OpenApiSchema mutable || mutable.Properties == null)
            return;

        foreach (string jsonName in _ignoredNames(context.Type))
        {
            if (mutable.Properties.Remove(jsonName))
                continue;

            string? caseAdjustedName = mutable.Properties.Keys.FirstOrDefault(name => string.Equals(name, jsonName, StringComparison.OrdinalIgnoreCase));

            if (caseAdjustedName is not null)
                mutable.Properties.Remove(caseAdjustedName);
        }
    }
}
