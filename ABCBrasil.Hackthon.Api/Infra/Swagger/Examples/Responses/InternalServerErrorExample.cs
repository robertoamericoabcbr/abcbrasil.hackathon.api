using ABCBrasil.Hackthon.Api.Domain.DataContracts.Responses;
using ABCBrasil.Hackthon.Api.Infra.Validations;
using Swashbuckle.AspNetCore.Filters;
using System;
using static ABCBrasil.Hackthon.Api.Infra.Commons.Constants.ApplicationConstants;

namespace ABCBrasil.Hackthon.Api.Infra.Swagger.Examples.Responses
{
    public class InternalServerErrorExample : IExamplesProvider<ApiResponseError>
    {
        public ApiResponseError GetExamples()
        {
            return new(Guid.NewGuid().ToString(), new ValidationNotification
            {
                Type = INTERNAL_SERVER_ERROR,
                Code = nameof(INTERNAL_SERVER_ERROR),
                Message = Messages.Validations.NOT_INFORMED
            });
        }
    }
}