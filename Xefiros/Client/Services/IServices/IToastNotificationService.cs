namespace Xefiros.Client.Services.IServices
{
    public interface IToastNotificationService
    {
        public void ShowSuccess(string details, string summary = "Operación Exitósa");
        public void ShowError(string details, string summary = "Operación Fallida");
        public void ShowInfo(string details, string summary = "Información");
        public void ShowWarning(string details, string summary = "Advertencia");
    }
}