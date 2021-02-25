using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Xefiros.DataAccess.Data.Repository.IRepository;
using Xefiros.Shared.Dtos;
using Xefiros.Shared.Models;
using Xefiros.Utility.Helpers;

namespace Xefiros.DataAccess.Data.Repository
{
    public class VentaRepository : Repository<Venta>, IVentasRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public VentaRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DataResponse<string>> Update(int ventaId, VentaCreateDto ventaCreateDto)
        {
            var ventaDb = await _context.Ventas.FindAsync(ventaId);

            if (ventaDb is null)
            {
                return new DataResponse<string>()
                {
                    Sussces = false,
                    Message = "No se encontró la venta para actualizar"
                };
            }

            _mapper.Map(ventaCreateDto, ventaDb);
            await _context.SaveChangesAsync();

            return new DataResponse<string>()
            {
                Sussces = true,
                Message = "Venta actualizada con éxito"
            };
        }

        public async Task<DataResponse<List<AbonoDto>>> ObtenerAbonos(int ventaId)
        {
            var ventaDb = await _context.Ventas.Include(x => x.Abonos).FirstOrDefaultAsync(x => x.Id == ventaId);

            if (ventaDb is null)
            {
                return new DataResponse<List<AbonoDto>>
                {
                    Message = "No se encontro la venta",
                    Sussces = false
                };
            }

            var abonosList = ventaDb.Abonos.Select(abono => new AbonoDto()
                    {Id = abono.Id, CantidadAbono = abono.CantidadAbono, FechaAbono = abono.FechaAbono})
                .ToList();

            return new DataResponse<List<AbonoDto>>
            {
                Data = abonosList,
                Sussces = true
            };
        }

        public async Task<DataResponse<string>> EliminarAbono(int abonoId)
        {
            var abonoDb = await _context.Abonos.FirstOrDefaultAsync(x => x.Id == abonoId);
            if (abonoDb is null)
            {
                return new DataResponse<string>
                {
                    Sussces = false,
                    Message = "No se encontró el abono para borrar"
                };
            }

            _context.Abonos.Remove(abonoDb);
            await _context.SaveChangesAsync();

            return new DataResponse<string>
            {
                Sussces = true,
                Message = "Abono borrado con éxito"
            };
        }

        public async Task<DataResponse<List<DetalleVentaDto>>> ObtenerDetalles(int ventaId)
        {
            var ventaDb = await _context.Ventas.Include(x => x.Detalles)
                .ThenInclude(x => x.Producto)
                .FirstOrDefaultAsync(x => x.Id == ventaId);
            if (ventaDb is null)
            {
                return new DataResponse<List<DetalleVentaDto>>()
                {
                    Sussces = false,
                    Message = "No se encotró la venta"
                };
            }

            return new DataResponse<List<DetalleVentaDto>>()
            {
                Sussces = true,
                Data = ventaDb.Detalles.Select(x => new DetalleVentaDto()
                {
                    Id = x.Id,
                    Cantidad = x.Cantidad,
                    Descuento = x.Descuento,
                    Descripcion = x.Producto.Nombre,
                    PrecioUnitario = x.Precio,
                    CodigoProducto = x.Producto.Codigo,
                    VentaId = x.VentaId,
                    Total = x.Total,
                    StockProducto = x.Producto.Stock,
                    ProductoId = x.ProductoId,
                    Impuesto = x.Venta.Impuesto
                }).ToList()
            };
        }

        public async Task<DataResponse<string>> EliminarDetalle(int detalleVentaId)
        {
            var detalleDb = await _context.DetallesVenta.FirstOrDefaultAsync(x => x.Id == detalleVentaId);
            if (detalleDb is null)
            {
                return new DataResponse<string>
                {
                    Sussces = false,
                    Message = "No se encontró el detalle para borrar"
                };
            }

            _context.DetallesVenta.Remove(detalleDb);
            await _context.SaveChangesAsync();

            return new DataResponse<string>
            {
                Sussces = true,
                Message = "Detalle borrado con éxito"
            };
        }

        public async Task<DataResponse<ResumenVentasPorTrimestreDto>> GetResumenVentasTrimestral()
        {
            var ventasAnioActual =
                await _context.Ventas.Where(x => x.FechaVenta.Year == DateTime.Now.Year && x.Estado == "pagada")
                    .ToListAsync();
            if (ventasAnioActual.Count == 0)
            {
                return new DataResponse<ResumenVentasPorTrimestreDto>()
                {
                    Sussces = true,
                    Data = new ResumenVentasPorTrimestreDto()
                    {
                        Anio = DateTime.Now.Year,
                        VentasQ1 = 0.0M,
                        VentasQ2 = 0.0M,
                        VentasQ3 = 0.0M,
                        VentasQ4 = 0.0M
                    }
                };
            }

            var ventasQ1 = ventasAnioActual.Where(x =>
                x.FechaVenta >= new DateTime(x.FechaVenta.Year, 1, 1) &&
                x.FechaVenta <= new DateTime(x.FechaVenta.Year, 3, 31)).Sum(t => t.Total);

            var ventasQ2 = ventasAnioActual.Where(x =>
                x.FechaVenta >= new DateTime(x.FechaVenta.Year, 4, 1) &&
                x.FechaVenta <= new DateTime(x.FechaVenta.Year, 6, 30)).Sum(t => t.Total);

            var ventasQ3 = ventasAnioActual.Where(x =>
                x.FechaVenta >= new DateTime(x.FechaVenta.Year, 7, 1) &&
                x.FechaVenta <= new DateTime(x.FechaVenta.Year, 9, 30)).Sum(t => t.Total);

            var ventasQ4 = ventasAnioActual.Where(x =>
                x.FechaVenta >= new DateTime(x.FechaVenta.Year, 10, 1) &&
                x.FechaVenta <= new DateTime(x.FechaVenta.Year, 12, 31)).Sum(t => t.Total);

            return new DataResponse<ResumenVentasPorTrimestreDto>()
            {
                Sussces = true,
                Data = new ResumenVentasPorTrimestreDto()
                {
                    Anio = DateTime.Now.Year,
                    VentasQ1 = ventasQ1,
                    VentasQ2 = ventasQ2,
                    VentasQ3 = ventasQ3,
                    VentasQ4 = ventasQ4
                }
            };
        }

        public async Task<ResumenVentasAnualesDto> GetResumenVentasAnual()
        {
            var ventasAnioAnterior =
                await _context.Ventas
                    .Where(x => x.FechaVenta.Year == DateTime.Now.AddYears(-1).Year && x.Estado == "pagada")
                    .ToListAsync();
            var ventasAnioActual =
                await _context.Ventas.Where(x => x.FechaVenta.Year == DateTime.Now.Year && x.Estado == "pagada")
                    .ToListAsync();
            var anioAnterior = ventasAnioAnterior.Select(x => x.FechaVenta.Year).FirstOrDefault();
            var anioActual = ventasAnioActual.Select(x => x.FechaVenta.Year).FirstOrDefault();

            return new ResumenVentasAnualesDto()
            {
                AnioAnterior = new List<ItemResumenAnualVenta>()
                {
                    new ItemResumenAnualVenta
                    {
                        Fecha = DateTime.Parse($"{anioAnterior}-01-01"),
                        TotalVentas = ventasAnioAnterior.Where(x => x.FechaVenta.Month == 1).Sum(t => t.Total)
                    },
                    new ItemResumenAnualVenta
                    {
                        Fecha = DateTime.Parse($"{anioAnterior}-02-01"),
                        TotalVentas = ventasAnioAnterior.Where(x => x.FechaVenta.Month == 2).Sum(t => t.Total)
                    },
                    new ItemResumenAnualVenta
                    {
                        Fecha = DateTime.Parse($"{anioAnterior}-03-01"),
                        TotalVentas = ventasAnioAnterior.Where(x => x.FechaVenta.Month == 3).Sum(t => t.Total)
                    },
                    new ItemResumenAnualVenta
                    {
                        Fecha = DateTime.Parse($"{anioAnterior}-04-01"),
                        TotalVentas = ventasAnioAnterior.Where(x => x.FechaVenta.Month == 4).Sum(t => t.Total)
                    },
                    new ItemResumenAnualVenta
                    {
                        Fecha = DateTime.Parse($"{anioAnterior}-05-01"),
                        TotalVentas = ventasAnioAnterior.Where(x => x.FechaVenta.Month == 5).Sum(t => t.Total)
                    },
                    new ItemResumenAnualVenta
                    {
                        Fecha = DateTime.Parse($"{anioAnterior}-06-01"),
                        TotalVentas = ventasAnioAnterior.Where(x => x.FechaVenta.Month == 6).Sum(t => t.Total)
                    },
                    new ItemResumenAnualVenta
                    {
                        Fecha = DateTime.Parse($"{anioAnterior}-07-01"),
                        TotalVentas = ventasAnioAnterior.Where(x => x.FechaVenta.Month == 7).Sum(t => t.Total)
                    },
                    new ItemResumenAnualVenta
                    {
                        Fecha = DateTime.Parse($"{anioAnterior}-08-01"),
                        TotalVentas = ventasAnioAnterior.Where(x => x.FechaVenta.Month == 8).Sum(t => t.Total)
                    },
                    new ItemResumenAnualVenta
                    {
                        Fecha = DateTime.Parse($"{anioAnterior}-09-01"),
                        TotalVentas = ventasAnioAnterior.Where(x => x.FechaVenta.Month == 9).Sum(t => t.Total)
                    },
                    new ItemResumenAnualVenta
                    {
                        Fecha = DateTime.Parse($"{anioAnterior}-10-01"),
                        TotalVentas = ventasAnioAnterior.Where(x => x.FechaVenta.Month == 10).Sum(t => t.Total)
                    },
                    new ItemResumenAnualVenta
                    {
                        Fecha = DateTime.Parse($"{anioAnterior}-11-01"),
                        TotalVentas = ventasAnioAnterior.Where(x => x.FechaVenta.Month == 11).Sum(t => t.Total)
                    },
                    new ItemResumenAnualVenta
                    {
                        Fecha = DateTime.Parse($"{anioAnterior}-12-01"),
                        TotalVentas = ventasAnioAnterior.Where(x => x.FechaVenta.Month == 12).Sum(t => t.Total)
                    }
                },
                AnioActual = new List<ItemResumenAnualVenta>()
                {
                    new ItemResumenAnualVenta
                    {
                        Fecha = DateTime.Parse($"{anioActual}-01-01"),
                        TotalVentas = ventasAnioActual.Where(x => x.FechaVenta.Month == 1).Sum(t => t.Total)
                    },
                    new ItemResumenAnualVenta
                    {
                        Fecha = DateTime.Parse($"{anioActual}-02-01"),
                        TotalVentas = ventasAnioActual.Where(x => x.FechaVenta.Month == 2).Sum(t => t.Total)
                    },
                    new ItemResumenAnualVenta
                    {
                        Fecha = DateTime.Parse($"{anioActual}-03-01"),
                        TotalVentas = ventasAnioActual.Where(x => x.FechaVenta.Month == 3).Sum(t => t.Total)
                    },
                    new ItemResumenAnualVenta
                    {
                        Fecha = DateTime.Parse($"{anioActual}-04-01"),
                        TotalVentas = ventasAnioActual.Where(x => x.FechaVenta.Month == 4).Sum(t => t.Total)
                    },
                    new ItemResumenAnualVenta
                    {
                        Fecha = DateTime.Parse($"{anioActual}-05-01"),
                        TotalVentas = ventasAnioActual.Where(x => x.FechaVenta.Month == 5).Sum(t => t.Total)
                    },
                    new ItemResumenAnualVenta
                    {
                        Fecha = DateTime.Parse($"{anioActual}-06-01"),
                        TotalVentas = ventasAnioActual.Where(x => x.FechaVenta.Month == 6).Sum(t => t.Total)
                    },
                    new ItemResumenAnualVenta
                    {
                        Fecha = DateTime.Parse($"{anioActual}-07-01"),
                        TotalVentas = ventasAnioActual.Where(x => x.FechaVenta.Month == 7).Sum(t => t.Total)
                    },
                    new ItemResumenAnualVenta
                    {
                        Fecha = DateTime.Parse($"{anioActual}-08-01"),
                        TotalVentas = ventasAnioActual.Where(x => x.FechaVenta.Month == 8).Sum(t => t.Total)
                    },
                    new ItemResumenAnualVenta
                    {
                        Fecha = DateTime.Parse($"{anioActual}-09-01"),
                        TotalVentas = ventasAnioActual.Where(x => x.FechaVenta.Month == 9).Sum(t => t.Total)
                    },
                    new ItemResumenAnualVenta
                    {
                        Fecha = DateTime.Parse($"{anioActual}-10-01"),
                        TotalVentas = ventasAnioActual.Where(x => x.FechaVenta.Month == 10).Sum(t => t.Total)
                    },
                    new ItemResumenAnualVenta
                    {
                        Fecha = DateTime.Parse($"{anioActual}-11-01"),
                        TotalVentas = ventasAnioActual.Where(x => x.FechaVenta.Month == 11).Sum(t => t.Total)
                    },
                    new ItemResumenAnualVenta
                    {
                        Fecha = DateTime.Parse($"{anioActual}-12-01"),
                        TotalVentas = ventasAnioActual.Where(x => x.FechaVenta.Month == 12).Sum(t => t.Total)
                    }
                }
            };
        }
    }
}