using ABCBrasil.Hackthon.Api.Domain.DataContracts.Responses;
using ABCBrasil.Hackthon.Api.Infra.Extensions;
using ABCBrasil.Hackthon.Api.Infra.Validations;
using ABCBrasil.Providers.BasicContractProvider.Lib;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using static ABCBrasil.Hackthon.Api.Infra.Commons.Constants.ApplicationConstants;

namespace ABCBrasil.Hackthon.Api.Infra.Controllers
{
    public class ApiControllerBase : ControllerBase
    {
        protected readonly INotificationManager _notificationManager;

        public ApiControllerBase(INotificationProvider notificationProvider)
        {
            _notificationManager = notificationProvider.CreateNotification();
        }

        protected BadRequestObjectResult BadRequest(string correlationId, List<NotificationBase> notifications)
        {
            return base.BadRequest(new ApiResponseError(notifications, correlationId));
        }

        protected NotFoundObjectResult NotFound(string correlationId, List<NotificationBase> notifications)
        {
            return NotFound(new ApiResponseError(notifications, correlationId));
        }

        protected UnprocessableEntityObjectResult UnprocessableEntity(string correlationId, List<NotificationBase> notifications)
        {
            return base.UnprocessableEntity(new ApiResponseError(notifications, correlationId));
        }

        protected CreatedResult Created<T>(string uri, T value)
        {
            return base.Created(uri, value);
        }

        protected OkObjectResult Ok<T>(T value)
        {
            return base.Ok(value);
        }

        protected IActionResult InternalServerError(string correlationId, List<NotificationBase> notifications = null)
        {
            notifications ??= _notificationManager.GetNotifications();

            return base.StatusCode(StatusCodes.Status500InternalServerError, new ApiResponseError(notifications, correlationId));
        }

        protected IActionResult ServiceUnavailable(string correlationId, List<NotificationBase> notifications = null)
        {
            notifications ??= _notificationManager.GetNotifications();

            return base.StatusCode(StatusCodes.Status503ServiceUnavailable, new ApiResponseError(notifications, correlationId));
        }

        protected IActionResult ValidateFailureReturn(string correlationId, List<NotificationBase> notifications = null)
        {
            if (notifications.HasBusinessValidation())
            {
                return UnprocessableEntity(correlationId, notifications);
            }

            if (notifications.HasIntegrationError())
            {
                return ServiceUnavailable(correlationId, notifications);
            }

            if (notifications.HasError())
            {
                return InternalServerError(correlationId, notifications);
            }

            if (notifications.HasNotFound())
            {
                return NotFound(correlationId, notifications);
            }

            return BadRequest(correlationId, notifications);
        }

        protected int GetStatusCode(IActionResult result)
        {
            if (result is IStatusCodeActionResult statusCodeResult)
            {
                return statusCodeResult.StatusCode ?? (int)HttpStatusCode.BadRequest;
            }

            return (int)HttpStatusCode.BadRequest;
        }

        protected async Task<(bool, List<NotificationBase>)> ValidateContractAsync<T>(T @object, IValidator<T> validator)
        {
            var context = new ValidationContext<object>(@object);
            ValidationResult validationResult = await validator.ValidateAsync(context);

            var notifications = new List<NotificationBase>();

            if (!validationResult.IsValid)
            {
                foreach (ValidationFailure error in validationResult.Errors)
                {
                    notifications.Add(new ValidationNotification
                    {
                        Type = CONTRACT_VALIDATION,
                        Param = error.PropertyName,
                        ParamType = @object.GetParamType(),
                        Code = error.ErrorCode,
                        Message = error.ErrorMessage
                    });
                }

                return (false, notifications);
            }

            return (true, new List<NotificationBase>());
        }

        protected async Task<(bool, List<NotificationBase>)> ValidateContractAsync<T, TValidator>(T @object)
            where TValidator : IValidator<T>, new()
            => await ValidateContractAsync(@object, new TValidator());
    }
}