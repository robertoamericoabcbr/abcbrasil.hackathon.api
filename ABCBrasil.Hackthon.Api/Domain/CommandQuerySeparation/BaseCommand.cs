using ABCBrasil.Hackthon.Api.Infra.Commons.Constants;
using ABCBrasil.Hackthon.Api.Infra.Extensions;
using ABCBrasil.Hackthon.Api.Infra.Validations;
using ABCBrasil.Providers.BasicContractProvider.Lib;
using FluentValidation;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABCBrasil.Hackthon.Api.Domain.CommandQuerySeparation
{
    public class BaseCommand
    {
        protected readonly INotificationManager _notificationManager;

        public BaseCommand(INotificationManager notificationManager)
        {
            _notificationManager = notificationManager;
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
                        Type = ApplicationConstants.CONTRACT_VALIDATION,
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