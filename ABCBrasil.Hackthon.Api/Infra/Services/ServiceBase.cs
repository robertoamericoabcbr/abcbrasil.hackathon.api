using ABCBrasil.Hackthon.Api.Infra.Commons.Constants;
using ABCBrasil.Hackthon.Api.Infra.Extensions;
using ABCBrasil.Hackthon.Api.Infra.Validations;
using ABCBrasil.Providers.BasicContractProvider.Lib;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ABCBrasil.Hackthon.Api.Infra.Services
{
    public class ServiceBase
    {
        protected INotificationManager _notificationManager;
        protected readonly IHttpContextAccessor _httpContextAccessor;

        public ServiceBase(
            INotificationManager notificationManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _notificationManager = notificationManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public ServiceBase(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void SetNotification(INotificationManager notificationManager)
        {
            _notificationManager = notificationManager;
        }

        protected async Task<bool> ValidateContractAsync<T>(T @object, IValidator<T> validator)
        {
            var sw = new Stopwatch();
            sw.Start();

            var context = new ValidationContext<object>(@object);
            ValidationResult validationResult = await validator.ValidateAsync(context);

            if (!validationResult.IsValid)
            {
                foreach (ValidationFailure error in validationResult.Errors)
                {
                    _notificationManager.AddNotification(new ValidationNotification
                    {
                        Type = ApplicationConstants.CONTRACT_VALIDATION,
                        Param = error.PropertyName,
                        ParamType = @object.GetParamType(),
                        Code = error.ErrorCode,
                        Message = error.ErrorMessage
                    });
                }

                return false;
            }

            return true;
        }

        protected async Task<bool> ValidateContractAsync<T, TValidator>(T @object)
            where TValidator : IValidator<T>, new() => await ValidateContractAsync(@object, new TValidator());
    }
}