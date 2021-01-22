using AutoMapper;
using Xefiros.Shared.Dtos;
using Xefiros.Shared.Models;

namespace Xefiros.DataAccess.MappingConf
{
    public class MapperProfile : Profile

    {
        public MapperProfile()
        {
            CreateMap<Cliente, ClienteDto>().ReverseMap();
            CreateMap<ProductoDto, Producto>().ForMember(x => x.Imagen, opt => opt.Ignore());
            CreateMap<Producto, ProductoDto>();
            CreateMap<Venta, VentaDto>();
            CreateMap<VentaCreateDto, Venta>();
        }
    }
}