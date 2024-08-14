using ABCBrasil.Hackthon.Api.Infra.Commons.Attributes;
using ABCBrasil.Hackthon.Api.Infra.Extensions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using static ABCBrasil.Hackthon.Api.Infra.Commons.Constants.ApplicationConstants;

namespace ABCBrasil.Hackthon.Api.Infra.Validations.Fluent
{
    public static class ValidationsExtensions
    {
        public static IRuleBuilderOptions<T, TProperty> OverrideCustomPropertyName<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule)
        {
            return rule.Configure(config =>
            {
                ParamNameAttribute paramNameAttr = config.Member.GetCustomAttribute<ParamNameAttribute>(false);

                if (paramNameAttr != null)
                {
                    config.PropertyName = paramNameAttr.ParamName;
                    return;
                }

                FromQueryAttribute fromQueryAttr = config.Member.GetCustomAttribute<FromQueryAttribute>(false);

                if (fromQueryAttr != null)
                {
                    config.PropertyName = fromQueryAttr.Name;
                    return;
                }

                config.PropertyName = config.PropertyName.ToCamelCase();
            });
        }

        public static IRuleBuilderOptions<T, TProperty> ValidatorMissing<T, TProperty>(this IRuleBuilderInitial<T, TProperty> ruleBuilder, string property)
        {
            return ruleBuilder
                    .NotEmpty()
                        .WithErrorCode(nameof(Messages.Validations.MISSING_FIELD))
                        .WithMessage(Messages.Validations.MISSING_FIELD)
                    .NotNull()
                        .WithErrorCode(nameof(Messages.Validations.MISSING_FIELD))
                        .WithMessage(Messages.Validations.MISSING_FIELD)
                    .OverridePropertyName(property);
        }
    }
}