using FluentValidation;
using FluentValidation.Results;

namespace ABCBrasil.Hackthon.Api.Infra.Validations.Fluent
{
    public abstract class ApiRequestValidator<T> : AbstractValidator<T>
    {
        protected override bool PreValidate(ValidationContext<T> context, ValidationResult result)
        {
            if (context.InstanceToValidate != null)
                return base.PreValidate(context, result);

            result.Errors.Add(new ValidationFailure("requestBody", "Request body is required."));

            return result.IsValid;
        }
    }
}