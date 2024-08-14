using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ABCBrasil.Hackthon.Api.Infra.Swagger
{
    public class SwaggerDecorationSchemaFilter : ISchemaFilter
    {
        private readonly IServiceProvider _serviceProvider;

        public SwaggerDecorationSchemaFilter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            ApplyTypeAnnotations(schema, context);

            // NOTE: It's possible for both MemberInfo and ParameterInfo to have non-null values - i.e. when the schema is for a property
            // within a class that is bound to a parameter. In this case, the MemberInfo should take precendence.

            if (context.MemberInfo != null)
            {
                ApplyMemberAnnotations(schema, context.MemberInfo);
            }
            else if (context.ParameterInfo != null)
            {
                ApplyParamAnnotations(schema, context.ParameterInfo);
            }
        }

        private void ApplyTypeAnnotations(OpenApiSchema schema, SchemaFilterContext context)
        {
            var schemaAttribute = context.Type.GetCustomAttributes<SwaggerSchemaDecorationAttribute>()
                .FirstOrDefault();

            if (schemaAttribute != null)
                ApplySchemaAttribute(schema, schemaAttribute);

            var schemaFilterAttribute = context.Type.GetCustomAttributes<SwaggerSchemaFilterAttribute>()
                .FirstOrDefault();

            if (schemaFilterAttribute != null)
            {
                var filter = (ISchemaFilter)ActivatorUtilities.CreateInstance(
                    _serviceProvider,
                    schemaFilterAttribute.Type,
                    schemaFilterAttribute.Arguments);

                filter.Apply(schema, context);
            }
        }

        private void ApplyParamAnnotations(OpenApiSchema schema, ParameterInfo parameterInfo)
        {
            var schemaAttribute = parameterInfo.GetCustomAttributes<SwaggerSchemaDecorationAttribute>()
                .FirstOrDefault();

            if (schemaAttribute != null)
                ApplySchemaAttribute(schema, schemaAttribute);
        }

        private void ApplyMemberAnnotations(OpenApiSchema schema, MemberInfo memberInfo)
        {
            var schemaAttribute = memberInfo.GetCustomAttributes<SwaggerSchemaDecorationAttribute>()
                .FirstOrDefault();

            if (schemaAttribute != null)
                ApplySchemaAttribute(schema, schemaAttribute);
        }

        private void ApplySchemaAttribute(OpenApiSchema schema, SwaggerSchemaDecorationAttribute schemaAttribute)
        {
            if (schemaAttribute.Description != null)
                schema.Description = schemaAttribute.Description;

            if (schemaAttribute.Format != null)
                schema.Format = schemaAttribute.Format;

            if (schemaAttribute.ReadOnlyFlag.HasValue)
                schema.ReadOnly = schemaAttribute.ReadOnlyFlag.Value;

            if (schemaAttribute.WriteOnlyFlag.HasValue)
                schema.WriteOnly = schemaAttribute.WriteOnlyFlag.Value;

            if (schemaAttribute.NullableFlag.HasValue)
                schema.Nullable = schemaAttribute.NullableFlag.Value;

            if (schemaAttribute.Required != null)
                schema.Required = new SortedSet<string>(schemaAttribute.Required);

            if (schemaAttribute.Title != null)
                schema.Title = schemaAttribute.Title;

            if (schemaAttribute.Pattern != null)
                schema.Pattern = schemaAttribute.Pattern;

            if (schemaAttribute.MaxLength > -1)
                schema.MaxLength = schemaAttribute.MaxLength;

            if (schemaAttribute.MinLength > -1)
                schema.MinLength = schemaAttribute.MinLength;
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.Enum, AllowMultiple = false)]
    public class SwaggerSchemaDecorationAttribute : Attribute
    {
        public SwaggerSchemaDecorationAttribute(string description = null)
        {
            Description = description;
        }

        public string Description { get; set; }

        public string Format { get; set; }

        public bool ReadOnly
        {
            get { throw new InvalidOperationException($"Use {nameof(ReadOnlyFlag)} instead"); }
            set { ReadOnlyFlag = value; }
        }

        public bool WriteOnly
        {
            get { throw new InvalidOperationException($"Use {nameof(WriteOnlyFlag)} instead"); }
            set { WriteOnlyFlag = value; }
        }

        public bool Nullable
        {
            get { throw new InvalidOperationException($"Use {nameof(NullableFlag)} instead"); }
            set { NullableFlag = value; }
        }

        public string[] Required { get; set; }

        public string Title { get; set; }

        public string Pattern { get; set; }

        public int MaxLength { get; set; } = -1;

        public int MinLength { get; set; } = -1;

        internal bool? ReadOnlyFlag { get; private set; }

        internal bool? WriteOnlyFlag { get; private set; }

        internal bool? NullableFlag { get; private set; }
    }
}