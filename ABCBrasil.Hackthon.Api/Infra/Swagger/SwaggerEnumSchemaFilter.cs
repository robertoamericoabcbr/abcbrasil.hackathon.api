using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Linq;

namespace ABCBrasil.Hackthon.Api.Infra.Swagger
{
    public class SwaggerEnumSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            var enumAttributes = context.Type.GetCustomAttributes(false).OfType<SwaggerEnumSchemaAttribute>();

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
                            var lstEnumNames = Enum.GetNames(enumAttribute.EnumType).ToList();

                            if (enumAttribute.ShowContentValue)
                            {
                                lstEnumNames.ForEach(name => propertySchemaEnum.Add(new OpenApiString($"{Convert.ToInt64(Enum.Parse(enumAttribute.EnumType, name))} = {name}")));
                            }
                            else
                            {
                                lstEnumNames.ForEach(name => propertySchemaEnum.Add(new OpenApiString($"{name}")));
                            }
                        }
                    }
                }
            }
        }
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public class SwaggerEnumSchemaAttribute : Attribute
    {
        public SwaggerEnumSchemaAttribute(string propertyName, Type enumType, bool showContentValue = false)
        {
            if (enumType != null && enumType.IsEnum)
                EnumType = enumType;

            PropertyName = propertyName ?? throw new ArgumentNullException(nameof(propertyName));
            ShowContentValue = showContentValue;
        }

        public Type EnumType { get; set; } = null;
        public string PropertyName { get; set; }
        public bool ShowContentValue { get; set; }
    }
}