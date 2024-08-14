using ABCBrasil.Hackthon.Api.Domain.DataContracts.Responses;
using ABCBrasil.Hackthon.Api.Infra.Commons.Constants;
using ABCBrasil.Hackthon.Api.Infra.Validations;
using Swashbuckle.AspNetCore.Filters;
using System;
using static ABCBrasil.Hackthon.Api.Infra.Commons.Constants.ApplicationConstants;

namespace ABCBrasil.Hackthon.Api.Infra.Swagger.Examples.Responses
{
    public class CreateUserBadRequestExample : IExamplesProvider<ApiResponseError>
    {
        public ApiResponseError GetExamples()
        {
            return new(Guid.NewGuid().ToString(), new ValidationNotification
            {
                Type = ApplicationConstants.CONTRACT_VALIDATION,
                ParamType = ApplicationConstants.TypeValidations.BODY,
                Message = Messages.Validations.MISSING_FIELD,
                Code = nameof(Messages.Validations.MISSING_FIELD)
            });
        }
    }
}