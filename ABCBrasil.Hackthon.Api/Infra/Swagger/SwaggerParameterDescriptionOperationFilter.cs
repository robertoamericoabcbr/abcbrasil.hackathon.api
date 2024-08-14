using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ABCBrasil.Hackthon.Api.Infra.Swagger
{
    public class SwaggerParameterDescriptionOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters = operation.Parameters ?? new List<OpenApiParameter>();

            var actionAttributes = context.MethodInfo.GetCustomAttributes(true);
            var controllerAttributes = context.MethodInfo.DeclaringType.GetTypeInfo().GetCustomAttributes(true);

            var attributes = actionAttributes.Union(controllerAttributes)
                .OfType<SwaggerParameterDescriptionAttribute>()
                .ToList();

            OpenApiParameter param;

            foreach (var attr in attributes)
            {
                param = operation.Parameters.FirstOrDefault(p => p.Name == attr.Name);

                if (param is null)
                {
                    operation.Parameters.Add(new OpenApiParameter()
                    {
                        Name = attr.Name,
                        In = attr.Location,
                        Description = attr.Description,
                        Required = attr.Required
                    });
                }
                else
                {
                    param.Description = attr.Description;
                }
            }
        }
    }

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public class SwaggerParameterDescriptionAttribute : Attribute
    {
        public SwaggerParameterDescriptionAttribute(string name, string description, ParameterLocation location = ParameterLocation.Query, bool required = false)
        {
            Name = name;
            Description = description;
            Location = location;
            Required = required;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public ParameterLocation Location { get; set; }
        public bool Required { get; set; }
    }
}