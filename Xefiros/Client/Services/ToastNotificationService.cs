using Radzen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xefiros.Client.Services.IServices;

namespace Xefiros.Client.Services
{
    public class ToastNotificationService : IToastNotificationService
    {
        private readonly NotificationService _notificationService;

        public ToastNotificationService(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public void ShowError(string details, string summary = "Operación Fallida")
        {
            _notificationService.Notify(new NotificationMessage() {Severity = NotificationSeverity.Error, Detail = details, Summary = summary });
        }

        public void ShowInfo(string details, string summary = "Información")
        {
            _notificationService.Notify(new NotificationMessage() { Severity = NotificationSeverity.Info, Detail = details, Summary = summary });
        }

        public void ShowSuccess(string details, string summary = "Operación Exitósa")
        {
            _notificationService.Notify(new NotificationMessage() { Severity = NotificationSeverity.Success, Detail = details, Summary = summary });
        }

        public void ShowWarning(string details, string summary = "Advertencia")
        {
            _notificationService.Notify(new NotificationMessage() { Severity = NotificationSeverity.Warning, Detail = details, Summary = summary });
        }
    }
}
