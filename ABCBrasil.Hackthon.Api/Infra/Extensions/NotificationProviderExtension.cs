using ABCBrasil.Hackthon.Api.Infra.Commons.Constants;
using ABCBrasil.Hackthon.Api.Infra.Validations;
using ABCBrasil.Providers.BasicContractProvider.Lib;
using System.Collections.Generic;

namespace ABCBrasil.Hackthon.Api.Infra.Extensions
{
    public static class NotificationProviderExtension
    {
        /// <summary>
        /// <see cref="HasIntegrationError(List{NotificationBase})"/>
        /// </summary>
        /// <param name="notifications"></param>
        /// <returns></returns>
        /// <author>
        /// Banco ABCBrasil - 2021
        /// </author>
        public static bool HasIntegrationError(this List<NotificationBase> notifications)
        {
            return notifications.HasNotification(ApplicationConstants.SERVICE_UNAVAILABLE);
        }

        /// <summary>
        /// <see cref="HasError(List{NotificationBase})"/>
        /// </summary>
        /// <param name="notifications"></param>
        /// <returns></returns>
        /// <author>
        /// Banco ABCBrasil - 2021
        /// </author>
        public static bool HasError(this List<NotificationBase> notifications)
        {
            return notifications.HasNotification(ApplicationConstants.ERROR);
        }

        public static bool HasNotFound(this List<NotificationBase> notifications)
        {
            return notifications.HasNotification(ApplicationConstants.NOT_FOUND);
        }

        /// <summary>
        /// <see cref="HasValidation(List{NotificationBase})"/>
        /// </summary>
        /// <param name="notifications"></param>
        /// <returns></returns>
        /// <author>
        /// Banco ABCBrasil - 2021
        /// </author>
        public static bool HasValidation(this List<NotificationBase> notifications)
        {
            return notifications.HasNotification(ApplicationConstants.CONTRACT_VALIDATION);
        }

        /// <summary>
        /// <see cref="HasBusinessValidation(List{NotificationBase})"/>
        /// </summary>
        /// <param name="notifications"></param>
        /// <returns></returns>
        /// <author>
        /// Banco ABCBrasil - 2021
        /// </author>
        public static bool HasBusinessValidation(this List<NotificationBase> notifications)
        {
            return notifications.HasNotification(ApplicationConstants.DOMAIN_VALIDATION);
        }

        /// <summary>
        /// <see cref="HasInternalServerError(List{NotificationBase})"/>
        /// </summary>
        /// <param name="notifications"></param>
        /// <returns></returns>
        /// <author>
        /// Banco ABCBrasil - 2021
        /// </author>
        public static bool HasInternalServerError(this List<NotificationBase> notifications)
        {
            return notifications.HasNotification(ApplicationConstants.INTERNAL_SERVER_ERROR);
        }

        /// <summary>
        /// <see cref="HasInfo(List{NotificationBase})"/>
        /// </summary>
        /// <param name="notifications"></param>
        /// <returns></returns>
        /// <author>
        /// Banco ABCBrasil - 2021
        /// </author>
        public static bool HasInfo(this List<NotificationBase> notifications)
        {
            return notifications.HasNotification(ApplicationConstants.INFO);
        }

        /// <summary>
        /// <see cref="AddValidationFail(INotificationManager, string, string, string, string)"/>
        /// </summary>
        /// <param name="notificationManager"></param>
        /// <param name="message"></param>
        /// <param name="code"></param>
        /// <param name="fieldName"></param>
        /// <param name="paramType"></param>
        /// <returns><see cref="INotificationManager"/></returns>
        /// /// <author>
        /// Banco ABCBrasil - 2021
        /// </author>
        public static INotificationManager AddValidationFail(this INotificationManager notificationManager, string message, string code = null, string fieldName = null, string paramType = null)
        {
            notificationManager.GetNotifications().Add(new ValidationNotification()
            {
                Type = ApplicationConstants.CONTRACT_VALIDATION,
                Message = message,
                Code = code ?? ApplicationConstants.CONTRACT_VALIDATION,
                Param = fieldName,
                ParamType = paramType,
            });

            return notificationManager;
        }

        /// <summary>
        /// <see cref="AddBusinessValidationFail(INotificationManager, string, string, string, string)"/>
        /// </summary>
        /// <param name="notificationManager"></param>
        /// <param name="message"></param>
        /// <param name="code"></param>
        /// <param name="fieldName"></param>
        /// <param name="paramType"></param>
        /// <returns><see cref="INotificationManager"/></returns>
        /// /// <author>
        /// Banco ABCBrasil - 2021
        /// </author>
        public static INotificationManager AddBusinessValidationFail(this INotificationManager notificationManager, string message, string code = null, string fieldName = null, string paramType = null)
        {
            notificationManager.GetNotifications().Add(new ValidationNotification()
            {
                Type = ApplicationConstants.DOMAIN_VALIDATION,
                Message = message,
                Code = code ?? ApplicationConstants.DOMAIN_VALIDATION,
                Param = fieldName,
                ParamType = paramType,
            });

            return notificationManager;
        }

        public static INotificationManager AddSingleBusinessValidationFail(this INotificationManager notificationManager, string message, string code = null, bool isIdempotenceFail = false)
        {
            notificationManager.GetNotifications().Add(new ValidationNotification()
            {
                Type = ApplicationConstants.DOMAIN_VALIDATION,
                Message = message,
                Code = code ?? ApplicationConstants.DOMAIN_VALIDATION,
                IsIdempotenceFail = isIdempotenceFail
            });

            return notificationManager;
        }

        private static bool HasNotification(this List<NotificationBase> notifications, string type)
        {
            return notifications.Exists(x => x.Type == type);
        }

        /// <summary>
        /// HasAnyNotificationError
        /// /// </summary>
        /// <param name="notifications"></param>
        /// <returns></returns>
        public static bool HasAnyNotificationError(this List<NotificationBase> notifications)
        {
            bool result = false;

            List<string> errors = new()
            {
                ApplicationConstants.ERROR,
                ApplicationConstants.SERVICE_UNAVAILABLE,
                ApplicationConstants.INTERNAL_SERVER_ERROR,
            };

            if (notifications.Exists(x => errors.Contains(x.Type)))
            {
                result = true;
            }

            return result;
        }

        public static INotificationManager AddNotFound(this INotificationManager notificationManager, string message, string code = null, string fieldName = null, string paramType = null)
        {
            notificationManager.GetNotifications().Add(new ValidationNotification()
            {
                Type = ApplicationConstants.NOT_FOUND,
                Message = message,
                Code = code ?? ApplicationConstants.NOT_FOUND,
                Param = fieldName,
                ParamType = paramType,
            });

            return notificationManager;
        }

        public static INotificationManager AddNotFound(this INotificationManager notificationManager)
        {
            notificationManager.GetNotifications().Add(new ValidationNotification()
            {
                Type = ApplicationConstants.NOT_FOUND
            });

            return notificationManager;
        }
    }
}