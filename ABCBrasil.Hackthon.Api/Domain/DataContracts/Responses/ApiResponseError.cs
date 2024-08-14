using ABCBrasil.Hackthon.Api.Infra.Commons.Constants;
using ABCBrasil.Hackthon.Api.Infra.Validations;
using ABCBrasil.Providers.BasicContractProvider.Lib;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ABCBrasil.Hackthon.Api.Domain.DataContracts.Responses
{
    [SwaggerSchema(Title = "ApiResponseError", Description = "Response de Erro")]
    public class ApiResponseError
    {
        [Required]
        public List<Error> Errors { get; } = [];

        [Required]
        public Meta Meta => new() { RequestDateTime = DateTime.Now, TotalRecords = Errors.Count };

        public ApiResponseError(string correlationId, params NotificationBase[] notifications)
        {
            foreach (NotificationBase notification in notifications.Where(p => p.Type != ApplicationConstants.INFO))
            {
                if (notification is ValidationNotification validation)
                {
                    Errors.Add(new Error(validation.Code, validation.Message, validation.Param, validation.ParamType));
                }
                else
                {
                    Errors.Add(new Error(notification.Type, null, notification.Message));
                }
            }

            Meta.CorrelationId = correlationId;
        }

        public ApiResponseError(IEnumerable<NotificationBase> notifications, string correlationId = null)
            : this(correlationId, notifications.ToArray())
        {
        }

        public ApiResponseError()
        {
        }
    }
}