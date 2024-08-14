using ABCBrasil.Hackthon.Api.Domain.DataContracts.Responses;
using ABCBrasil.Hackthon.Api.Infra.Validations;
using Swashbuckle.AspNetCore.Filters;
using System;
using static ABCBrasil.Hackthon.Api.Infra.Commons.Constants.ApplicationConstants;

namespace ABCBrasil.Hackthon.Api.Infra.Swagger.Examples.Responses
{
    public class UpdateUserUnprocessableEntityExample : IExamplesProvider<ApiResponseError>
    {
        public ApiResponseError GetExamples()
        {
            return new(Guid.NewGuid().ToString(), new ValidationNotification
            {
                Type = DOMAIN_VALIDATION,
                Code = nameof(Messages.Validations.INVALID_FIELD_FORMAT),
                ParamType = TypeValidations.BODY,
                Message = Messages.Validations.INVALID_FIELD_FORMAT
            });
        }
    }
}