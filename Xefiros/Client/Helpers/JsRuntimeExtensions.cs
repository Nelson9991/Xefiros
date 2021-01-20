using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace Xefiros.Client.Helpers
{
    public static class JsRuntimeExtensions
    {
        public static async ValueTask ToastrSuccess(this IJSRuntime jsRuntime, string message)
        {
            await jsRuntime.InvokeVoidAsync("showToastr", "success", message);
        }

        public static async ValueTask ToastrFailure(this IJSRuntime jsRuntime, string message)
        {
            await jsRuntime.InvokeVoidAsync("showToastr", "error", message);
        }

        public static async ValueTask SwalSuccess(this IJSRuntime jsRuntime, string message, string title)
        {
            await jsRuntime.InvokeVoidAsync("showSwal", "success", message, title);
        }

        public static async ValueTask SwalFailure(this IJSRuntime jsRuntime, string message, string title)
        {
            await jsRuntime.InvokeVoidAsync("showSwal", "error", message, title);
        }

        public static async ValueTask<bool> SwalConfirmation(this IJSRuntime jsRuntime, string message)
        {
            return await jsRuntime.InvokeAsync<bool>("swalConfirmation", message);
        }

        public static void SwalShowLoading(this IJSInProcessRuntime jsRuntime)
        {
            jsRuntime.InvokeVoid("activateLoading");
        }

        public static void SwalCloseLoading(this IJSInProcessRuntime jsRuntime)
        {
            jsRuntime.InvokeVoid("closeLoading");
        }
    }
}