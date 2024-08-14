using ABCBrasil.Hackthon.Api.Infra.Extensions;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Reflection;

namespace ABCBrasil.Hackthon.Api.Infra.Swagger
{
    public class SwaggerParameterArrayValuesParameterFilter : IParameterFilter
    {
        public void Apply(OpenApiParameter parameter, ParameterFilterContext context)
        {
            if (context.PropertyInfo != null)
            {
                var enumAttribute = context.PropertyInfo.GetCustomAttribute<SwaggerParameterArrayValuesAttribute>();

                if (enumAttribute != null)
                {
                    OpenApiSchema propertySchemaEnum = parameter.Schema;

                    propertySchemaEnum.Enum.Clear();

                    if (enumAttribute.IsIntegerValues)
                    {
                        enumAttribute.Values.ForEach(value => propertySchemaEnum.Enum.Add(new OpenApiString($"{(long)value}")));
                    }
                    else
                    {
                        enumAttribute.Values.ForEach(value => propertySchemaEnum.Enum.Add(new OpenApiString($"{value}")));
                    }
                }
            }
        }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter, Inherited = false, AllowMultiple = false)]
    public class SwaggerParameterArrayValuesAttribute : Attribute
    {
        public SwaggerParameterArrayValuesAttribute(object[] values, bool isIntegerValues = false)
        {
            Values = values;
            IsIntegerValues = isIntegerValues;
        }

        public bool IsIntegerValues { get; set; }
        public object[] Values { get; set; }
    }
}