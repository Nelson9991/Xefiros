using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Xefiros.Shared;
using Xefiros.Shared.Dtos;
using Xefiros.Utility.Helpers;

namespace Xefiros.DataAccess.Data.Repository.IRepository
{
    public interface IRepository<TSource>
        where TSource : class
    {
        Task<DataResponse<TResponse>> Get<TResponse>(int id) where TResponse : class;

        Task<List<TResponse>> GetAllNoPaging<TResponse>(
            Expression<Func<TSource, bool>> filter = null,
            Func<IQueryable<TSource>, IOrderedQueryable<TSource>> orderBy = null,
            string includeProperties = null
        );

        Task<ApiResponseDto<TResponse>> GetAllWithPaging<TResponse>(
            int pageIndex = 0,
            int pageSize = 10,
            string filterColumn = null,
            string filterQuery = null,
            string includeProperties = null
        );

        Task<DataResponse<TResponse>> GetFirstOrDefault<TResponse>(Expression<Func<TSource, bool>> filter = null,
            string includeProperties = null);

        Task Add<TDto>(TDto dto);

        Task<DataResponse<TSource>> Remove(int id);
        void Remove(TSource entity);
    }
}