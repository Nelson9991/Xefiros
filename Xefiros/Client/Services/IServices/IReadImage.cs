using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;

namespace Xefiros.Client.Services.IServices
{
    public interface IReadImage
    {
        public Task<(bool, string)> ReadImageAsync(IBrowserFile file, string fileName);
    }
}