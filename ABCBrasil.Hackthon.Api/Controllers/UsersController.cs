using ABCBrasil.Hackthon.Api.Domain.CommandQuerySeparation.Commands;
using ABCBrasil.Hackthon.Api.Domain.CommandQuerySeparation.Queries;
using ABCBrasil.Hackthon.Api.Domain.DataContracts.Requests.Users;
using ABCBrasil.Hackthon.Api.Domain.DataContracts.Responses;
using ABCBrasil.Hackthon.Api.Domain.DataContracts.Responses.Users;
using ABCBrasil.Hackthon.Api.Infra.Commons.Constants;
using ABCBrasil.Hackthon.Api.Infra.Controllers;
using ABCBrasil.Hackthon.Api.Infra.Swagger;
using ABCBrasil.Hackthon.Api.Infra.Swagger.Examples.Responses;
using ABCBrasil.Providers.BasicContractProvider.Lib;
using ABCBrasil.Providers.CommonProvider.Lib;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using static ABCBrasil.Hackthon.Api.Infra.Commons.Constants.ApplicationConstants;

namespace ABCBrasil.Hackthon.Api.Controllers
{
    [ApiController]
    [ApiVersion(API_VERSION_1)]
    [Route(ROUTE_DEFAULT_CONTROLLER)]
    [SwaggerParameterDescription("version", "Versão da api")]
    [Produces("application/json")]
    public class UsersController : ApiControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public UsersController(
            INotificationProvider notificationProvider,
            IMapper mapper,
            IMediator mediator)
            : base(notificationProvider)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        /// <summary>
        /// Exclui um usuário pelo id
        /// </summary>
        /// <param name="query"></param>
        /// <returns>Retorna sucesso sem conteúdo</returns>
        /// <response code="204">Retorna sucesso sem conteúdo</response>
        /// <response code="404">usuário não encontrado</response>
        /// <response code="500">Erro interno do servidor</response>
        /// <response code="503">Um ou mais serviços internos indisponíveis</response>
        [HttpDelete(ApplicationConstants.ROUTE_DEFAULT_ID)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponseError))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponseError))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponseError))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(ApiResponseError))]
        public async Task<IActionResult> Delete([FromRoute] DeleteUserCommandQuery query)
        {
            Try<List<NotificationBase>, ApiResponse<bool>> result = await _mediator.Send(query);

            IActionResult response = result
                .Match(
                    failure: (failure) =>
                    {
                        string correlationId = Guid.NewGuid().ToString();
                        var failureResponse = ValidateFailureReturn(correlationId, result.GetFailure());
                        return failureResponse;
                    },
                    success: (success) =>
                    {
                        return NoContent();
                    }
            );

            return response;
        }

        /// <summary>
        /// Obter um usuário pelo id
        /// </summary>
        /// <param name="query"></param>
        /// <returns>Retorna o usuário criado</returns>
        /// <response code="200">Retorna o usuário</response>
        /// <response code="404">usuário não encontrado</response>
        /// <response code="500">Erro interno do servidor</response>
        /// <response code="503">Um ou mais serviços internos indisponíveis</response>
        [HttpGet(ApplicationConstants.ROUTE_DEFAULT_ID)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<CreateUserResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponseError))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponseError))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponseError))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(ApiResponseError))]
        public async Task<IActionResult> Get([FromRoute] GetUserCommandQuery query)
        {
            Try<List<NotificationBase>, ApiResponse<CreateUserResponse>> result = await _mediator.Send(query);

            IActionResult response = result
                .Match(
                    failure: (failure) =>
                    {
                        string correlationId = Guid.NewGuid().ToString();
                        var failureResponse = ValidateFailureReturn(correlationId, result.GetFailure());
                        return failureResponse;
                    },
                    success: (success) =>
                    {
                        return Ok(result.GetSuccess());
                    }
            );

            return response;
        }

        /// <summary>
        /// Cria um usuário
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Uma resposta padrão com os dados do usuário criado</returns>
        /// <response code="201">Uma resposta padrão com os dados do usuário criado</response>
        /// <response code="400">
        ///     Payload inválido.
        ///     <pre>
        ///          - INVALID_FIELD_FORMAT - O formato do campo é inválido.
        ///     </pre>
        /// </response>
        /// <response code="422">
        /// <pre>
        /// Erro de negócio.
        ///  - INVALID_FIELD_VALUE - O valor informado é inválido
        /// </pre>
        /// </response>
        /// <response code="500">Erro interno do servidor</response>
        /// <response code="503">Um ou mais serviços internos indisponíveis</response>
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ApiResponse<CreateUserResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponseError))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponseError))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(ApiResponseError))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(CreateUserBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status422UnprocessableEntity, typeof(CreateUserUnprocessableEntityExample))]
        public async Task<IActionResult> Post([FromBody] CreateUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(statusCode: (int)HttpStatusCode.BadRequest);
            }
            else
            {
                var command = _mapper.Map<CreateUserCommandRequest>(request);

                Try<List<NotificationBase>, ApiResponse<CreateUserResponse>> result = await _mediator.Send(command);

                IActionResult response = result
                    .Match(
                        failure: (failure) =>
                        {
                            string correlationId = Guid.NewGuid().ToString();
                            var failureResponse = ValidateFailureReturn(correlationId, result.GetFailure());
                            return failureResponse;
                        },
                        success: (success) =>
                        {
                            return Created(string.Empty, result.GetSuccess());
                        }
                );

                return response;
            }
        }

        /// <summary>
        /// Atualiza um usuário
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns>Uma resposta padrão com os dados do usuário atualizado</returns>
        /// <response code="201">Uma resposta padrão com os dados do usuário atualizado</response>
        /// <response code="400">
        ///     Payload inválido.
        ///     <pre>
        ///          - INVALID_FIELD_FORMAT - O formato do campo é inválido.
        ///     </pre>
        /// </response>
        /// <response code="422">
        /// <pre>
        /// Erro de negócio.
        ///  - INVALID_FIELD_VALUE - O valor informado é inválido
        /// </pre>
        /// </response>
        /// <response code="500">Erro interno do servidor</response>
        /// <response code="503">Um ou mais serviços internos indisponíveis</response>
        [HttpPut(ApplicationConstants.ROUTE_DEFAULT_ID)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ApiResponse<UpdateUserResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponseError))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponseError))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(ApiResponseError))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(UpdateUserBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status422UnprocessableEntity, typeof(UpdateUserUnprocessableEntityExample))]
        public async Task<IActionResult> Put([FromRoute] string id, [FromBody] UpdateUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(statusCode: (int)HttpStatusCode.BadRequest);
            }
            else
            {
                var command = _mapper.Map<UpdateUserCommandRequest>(request);

                command.Id = id;

                Try<List<NotificationBase>, ApiResponse<UpdateUserResponse>> result = await _mediator.Send(command);

                IActionResult response = result
                    .Match(
                        failure: (failure) =>
                        {
                            string correlationId = Guid.NewGuid().ToString();
                            var failureResponse = ValidateFailureReturn(correlationId, result.GetFailure());
                            return failureResponse;
                        },
                        success: (success) =>
                        {
                            return Created(string.Empty, result.GetSuccess());
                        }
                );

                return response;
            }
        }
    }
}