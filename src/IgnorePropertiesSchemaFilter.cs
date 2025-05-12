using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using Soenneker.Swashbuckle.Attributes.IgnoreProperty;

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
    /// <summary>
    /// Applies the filter by removing properties from the generated OpenAPI schema
    /// that have the <see cref="OpenApiIgnoreProperty"/>.
    /// </summary>
    /// <param name="schema">The OpenAPI schema being generated.</param>
    /// <param name="context">The context for schema generation, including the target type.</param>
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type.GetProperties() is not {Length: > 0} props)
            return;

        foreach (PropertyInfo prop in props)
        {
            if (prop.GetCustomAttribute<OpenApiIgnoreProperty>() == null)
                continue;

            string jsonName = prop.Name;
            schema.Properties.Remove(jsonName);
        }
    }
}