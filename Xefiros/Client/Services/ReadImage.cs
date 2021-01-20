using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;
using Xefiros.Client.Services.IServices;

namespace Xefiros.Client.Services
{
    public class ReadImage : IReadImage
    {
        public async Task<(bool, string)> ReadImageAsync(IBrowserFile file, string fileName)
        {
            var fileInfo = new FileInfo(fileName);
            if (fileInfo.Extension.ToLower() == ".jpg" ||
                fileInfo.Extension.ToLower() == ".png" ||
                fileInfo.Extension.ToLower() == ".jpeg")
            {
                var bytesImagen = new byte[file.Size];

                await file.OpenReadStream(maxAllowedSize: 1000000).ReadAsync(bytesImagen);

                return (true, (Convert.ToBase64String(bytesImagen)));
            }
            else
            {
                return (false, "Solo se permiten los formatos: \".jpg/.jpeg/.png\"");
            }
        }
    }
}