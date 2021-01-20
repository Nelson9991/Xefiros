using System.Threading.Tasks;

namespace Xefiros.DataAccess.Services.IServices
{
    public interface IFileUpload
    {
        public Task<string> GuardarArchivo(byte[] contenido, string extension, string nombreContenedor);
        public Task EliminarArchivo(string ruta, string nombreContenedor);
        public Task<string> EditarArchivo(byte[] contenido, string extension, string nombreContenedor, string ruta);
    }
}