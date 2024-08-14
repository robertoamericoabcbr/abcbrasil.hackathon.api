using ABCBrasil.Hackthon.Api.Infra.Validations;
using ABCBrasil.Providers.BasicContractProvider.Lib;
using System.Collections.Generic;
using static ABCBrasil.Hackthon.Api.Infra.Commons.Constants.ApplicationConstants;

namespace ABCBrasil.Hackthon.Api.Infra.Extensions
{
    public static class NotificationManagerExtensions
    {
        public static INotificationManager AddNotification(this INotificationManager notificationManager, NotificationBase notification)
        {
            var notifications = new List<NotificationBase> { notification };
            notificationManager.AddNotifications(notifications);

            return notificationManager;
        }

        public static INotificationManager AddServiceUnavailable(this INotificationManager notificationManager)
        {
            notificationManager.AddNotification(new ValidationNotification
            {
                Type = SERVICE_UNAVAILABLE,
                Code = nameof(SERVICE_UNAVAILABLE),
                Message = Messages.Validations.SERVICE_UNAVAILABLE_MESSAGE
            });

            return notificationManager;
        }
    }
}