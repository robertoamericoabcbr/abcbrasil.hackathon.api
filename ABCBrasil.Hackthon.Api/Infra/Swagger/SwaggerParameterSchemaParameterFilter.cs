using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Reflection;

namespace ABCBrasil.Hackthon.Api.Infra.Swagger
{
    public class SwaggerParameterSchemaParameterFilter : IParameterFilter
    {
        public void Apply(OpenApiParameter parameter, ParameterFilterContext context)
        {
            if (context.PropertyInfo != null)
            {
                var attribute = context.PropertyInfo.GetCustomAttribute<SwaggerParameterSchemaAttribute>();

                if (attribute != null)
                {
                    parameter.Schema.Example = new OpenApiString(attribute.FillUpWithCurrentDate ? DateTime.UtcNow.AddHours(-3).ToString(attribute.FormatToCurrentDate) : attribute.Example);
                    parameter.Schema.Format = attribute.Format;
                    parameter.Schema.Pattern = attribute.Pattern;
                    parameter.Required = attribute.Required;
                }
            }
        }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter, Inherited = false, AllowMultiple = false)]
    public class SwaggerParameterSchemaAttribute : Attribute
    {
        public string Example { get; set; }
        public string Format { get; set; }
        public string Pattern { get; set; }
        public string FormatToCurrentDate { get; set; } = "yyyy’-‘MM’-‘dd’T’HH’:’mm’:’ss.fffffffK";
        public bool FillUpWithCurrentDate { get; set; } = false;
        public bool Required { get; set; }
    }
}