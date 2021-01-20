using System;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using Xefiros.DataAccess.Services.IServices;

namespace Xefiros.DataAccess.Services
{
    public class AzureFileUpload : IFileUpload
    {
        private readonly string _connectionString;

        public AzureFileUpload(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("AzureStorage");
        }

        public async Task<string> GuardarArchivo(byte[] contenido, string extension, string nombreContenedor)
        {
            var contenedor = new BlobContainerClient(_connectionString, nombreContenedor);

            var nombreArchivo = $"{Guid.NewGuid()}{extension}";

            await using var ms = new MemoryStream(contenido);

            await contenedor.UploadBlobAsync(nombreArchivo, ms);

            var blob = contenedor.GetBlobClient(nombreArchivo);

            return blob.Uri.ToString();
        }

        public async Task EliminarArchivo(string ruta, string nombreContenedor)
        {
            if (string.IsNullOrEmpty(ruta))
            {
                return;
            }

            var cliente = new BlobContainerClient(_connectionString, nombreContenedor);
            var nombreArchivo = Path.GetFileName(ruta);
            var blob = cliente.GetBlobClient(nombreArchivo);
            await blob.DeleteIfExistsAsync();
        }

        public async Task<string> EditarArchivo(byte[] contenido, string extension, string nombreContenedor,
            string ruta)
        {
            if (!string.IsNullOrEmpty(ruta)) await EliminarArchivo(ruta, nombreContenedor);

            return await GuardarArchivo(contenido, extension, nombreContenedor);
        }
    }
}