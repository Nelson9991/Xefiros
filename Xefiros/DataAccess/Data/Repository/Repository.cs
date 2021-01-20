﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Xefiros.DataAccess.Data.Repository.IRepository;
using Xefiros.Shared;
using Xefiros.Shared.Dtos;
using Xefiros.Utility.Helpers;

namespace Xefiros.DataAccess.Data.Repository
{
    public class Repository<TSource> : IRepository<TSource>
        where TSource : class
    {
        internal DbSet<TSource> DbSet;
        private readonly IMapper _mapper;

        public Repository(DbContext context, IMapper mapper)
        {
            _mapper = mapper;
            DbSet = context.Set<TSource>();
        }

        public async Task<DataResponse<TResponse>> Get<TResponse>(int id)
            where TResponse : class
        {
            var entity = await DbSet.FindAsync(id);

            if (entity is null)
            {
                return new DataResponse<TResponse>
                {
                    Data = null,
                    Sussces = false,
                    Message = "No se encontro el Cliente"
                };
            }

            return new DataResponse<TResponse>
            {
                Data = _mapper.Map<TResponse>(entity),
                Sussces = true
            };
        }

        public async Task<List<TResponse>> GetAll<TResponse>(Expression<Func<TSource, bool>> filter = null,
            Func<IQueryable<TSource>, IOrderedQueryable<TSource>> orderBy = null, string includeProperties = null)
        {
            IQueryable<TSource> query = DbSet;

            if (filter is not null)
            {
                query = query.Where(filter);
            }

            if (includeProperties is not null)
            {
                foreach (var includeProperty in includeProperties.Split(new[] {','},
                    StringSplitOptions.RemoveEmptyEntries))
                {
                    if (PropertyValidator.IsValidProperty<TSource>(includeProperty))
                    {
                        query.Include(includeProperty);
                    }
                }
            }

            if (orderBy is not null)
            {
                return _mapper.Map<List<TResponse>>(await orderBy(query).ToListAsync());
            }

            return _mapper.Map<List<TResponse>>(await query.ToListAsync());
        }

        public async Task<ApiResponseDto<TResponse>> GetAll<TResponse>(
            int pageIndex = 0,
            int pageSize = 10,
            string filterColumn = null,
            string filterQuery = null,
            string includeProperties = null)
        {
            IQueryable<TSource> queryable = DbSet;

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new[] {','},
                    StringSplitOptions.RemoveEmptyEntries))
                {
                    if (PropertyValidator.IsValidProperty<TSource>(includeProperty))
                    {
                        queryable.Include(includeProperty);
                    }
                }
            }

            var apiResult = await ApiResult<TSource, TResponse>.CreateAsync(
                queryable,
                pageIndex,
                pageSize,
                _mapper,
                filterColumn,
                filterQuery);

            return new ApiResponseDto<TResponse>()
            {
                DataResult = apiResult.DataResult,
                FilterColumn = apiResult.FilterColumn,
                FilterQuery = apiResult.FilterQuery,
                PageSize = apiResult.PageSize,
                PageIndex = apiResult.PageIndex,
                TotalCount = apiResult.TotalCount,
                TotalPages = apiResult.TotalPages
            };
        }

        public async Task<TResponse> GetFirstOrDefault<TResponse>(Expression<Func<TSource, bool>> filter = null,
            string includeProperties = null)
        {
            IQueryable<TSource> queryable = DbSet;

            if (filter != null)
            {
                queryable = queryable.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new[] {','},
                    StringSplitOptions.RemoveEmptyEntries))
                {
                    queryable.Include(includeProperty);
                }
            }

            return _mapper.Map<TResponse>(await queryable.FirstOrDefaultAsync());
        }

        public async Task Add<TDto>(TDto dto)
        {
            var entity = _mapper.Map<TSource>(dto);
            await DbSet.AddAsync(entity);
        }

        public async Task<DataResponse<string>> Remove(int id)
        {
            TSource entityToRemove = await DbSet.FindAsync(id);

            if (entityToRemove is null)
            {
                return new DataResponse<string>
                {
                    Sussces = false,
                    Message = "No se encontro el Cliente"
                };
            }

            Remove(entityToRemove);

            return new DataResponse<string>
            {
                Sussces = true,
                Message = "Cliente borrado con exito",
            };
        }

        public void Remove(TSource entity)
        {
            DbSet.Remove(entity);
        }
    }
}