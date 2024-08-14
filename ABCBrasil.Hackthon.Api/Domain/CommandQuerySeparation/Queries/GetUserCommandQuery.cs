using ABCBrasil.Hackthon.Api.Domain.DataContracts.Responses;
using ABCBrasil.Hackthon.Api.Domain.DataContracts.Responses.Users;
using ABCBrasil.Hackthon.Api.Infra.Commons.Attributes;
using ABCBrasil.Hackthon.Api.Infra.Commons.Constants;
using ABCBrasil.Hackthon.Api.Infra.Swagger;
using ABCBrasil.Providers.BasicContractProvider.Lib;
using ABCBrasil.Providers.CommonProvider.Lib;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;

namespace ABCBrasil.Hackthon.Api.Domain.CommandQuerySeparation.Queries
{
    [ParamType(ApplicationConstants.TypeValidations.ROUTER)]
    [SwaggerSchema("Payload para obter um usuário", Title = "GetUserRequest", Required = ["Id"])]
    public class GetUserCommandQuery : IRequest<Try<List<NotificationBase>, ApiResponse<CreateUserResponse>>>
    {
        /// <summary>
        /// Identificador do usuário.
        /// </summary>
        /// <example>b1f56932-073e-4e5c-a358-cc36b0967697</example>
        [SwaggerSchemaDecoration(Nullable = false)]
        public string Id { get; set; }
    }
}