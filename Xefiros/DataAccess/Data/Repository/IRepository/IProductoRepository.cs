using System.Collections.Generic;
using System.Threading.Tasks;
using Xefiros.Shared.Dtos;
using Xefiros.Shared.Models;
using Xefiros.Utility.Helpers;

namespace Xefiros.DataAccess.Data.Repository.IRepository
{
    public interface IProductoRepository : IRepository<Producto>
    {
        public Task<DataResponse<string>> Update(int prodId, ProductoDto prodDto);
        public Task<bool> ExisteProductoCodigo(string codigo, int prodId = 0);
        public Task AddProducto(ProductoDto productoDto);
        public Task<DataResponse<Producto>> RemoveProductoWithImage(int id);
        public Task<List<ProductoForVentaDto>> ObtenerProductosParaVenta();
        public Task<DataResponse<string>> DisminuirStock(int prodId, int cantidadDisminuir);
    }
}