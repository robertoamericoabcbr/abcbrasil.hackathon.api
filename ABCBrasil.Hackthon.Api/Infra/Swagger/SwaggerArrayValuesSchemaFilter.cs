using ABCBrasil.Hackthon.Api.Infra.Extensions;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Linq;

namespace ABCBrasil.Hackthon.Api.Infra.Swagger
{
    public class SwaggerArrayValuesSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            var enumAttributes = context.Type.GetCustomAttributes(false).OfType<SwaggerArrayValuesSchemaAttribute>();

            if (enumAttributes.Any())
            {
                foreach (var enumAttribute in enumAttributes)
                {
                    var propertySchemas = schema.Properties.Where(p => p.Key.ToLower() == enumAttribute.PropertyName.ToLower());

                    if (propertySchemas.Any())
                    {
                        foreach (var propertySchemaEnum in propertySchemas.Select(ps => ps.Value.Enum))
                        {
                            propertySchemaEnum.Clear();

                            if (enumAttribute.IsIntegerValues)
                            {
                                enumAttribute.Values.ForEach(value => propertySchemaEnum.Add(new OpenApiString($"{(long)value}")));
                            }
                            else
                            {
                                enumAttribute.Values.ForEach(value => propertySchemaEnum.Add(new OpenApiString($"{value}")));
                            }
                        }
                    }
                }
            }
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class SwaggerArrayValuesSchemaAttribute : Attribute
    {
        public SwaggerArrayValuesSchemaAttribute(string propertyName, object[] values, bool isIntegerValues = false)
        {
            Values = values;
            IsIntegerValues = isIntegerValues;
            PropertyName = propertyName;
        }

        public string PropertyName { get; set; }
        public bool IsIntegerValues { get; set; }
        public object[] Values { get; set; }
    }
}