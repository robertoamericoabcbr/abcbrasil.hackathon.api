using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Linq;
using System.Reflection;

namespace ABCBrasil.Hackthon.Api.Infra.Swagger
{
    public class SwaggerParameterEnumParameterFilter : IParameterFilter
    {
        public void Apply(OpenApiParameter parameter, ParameterFilterContext context)
        {
            if (context.PropertyInfo != null)
            {
                var enumAttribute = context.PropertyInfo.GetCustomAttribute<SwaggerParameterEnumAttribute>();

                if (enumAttribute != null)
                {
                    OpenApiSchema propertySchemaEnum = parameter.Schema;

                    propertySchemaEnum.Enum.Clear();
                    var lstEnumNames = Enum.GetNames(enumAttribute.EnumType).ToList();

                    if (enumAttribute.ShowContentValue)
                    {
                        lstEnumNames.ForEach(name => propertySchemaEnum.Enum.Add(new OpenApiString($"{Convert.ToInt64(Enum.Parse(enumAttribute.EnumType, name))} = {name}")));
                    }
                    else
                    {
                        lstEnumNames.ForEach(name => propertySchemaEnum.Enum.Add(new OpenApiString($"{name}")));
                    }
                }
            }
        }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter, Inherited = false, AllowMultiple = false)]
    public class SwaggerParameterEnumAttribute : Attribute
    {
        public SwaggerParameterEnumAttribute(Type enumType, bool showContentValue = false)
        {
            if (enumType != null && enumType.IsEnum)
                EnumType = enumType;

            ShowContentValue = showContentValue;
        }

        public Type EnumType { get; set; } = null;
        public bool ShowContentValue { get; set; }
    }
}