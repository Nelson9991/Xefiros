using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Xefiros.Utility.Helpers;

namespace Xefiros.Shared
{
    public class ApiResult<TSource, TResponse>
    {
        private ApiResult(
            List<TResponse> dataResult,
            int count,
            int pageIndex,
            int pageSize,
            string sortColumn,
            string sortOrder,
            string filterColumn,
            string filterQuery)
        {
            DataResult = dataResult;
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = count;
            TotalPages = (int) Math.Ceiling(count / (double) pageSize);
            SortColumn = sortColumn;
            SortOrder = sortOrder;
            FilterColumn = filterColumn;
            FilterQuery = filterQuery;
        }

        #region Methods

        public static async Task<ApiResult<TSource, TResponse>> CreateAsync(
            IQueryable<TSource> source,
            int pageIndex,
            int pageSize,
            IMapper mapper,
            string sortColumn = null,
            string sortOrder = null,
            string filterColumn = null,
            string filterQuery = null)
        {
            if (!string.IsNullOrEmpty(filterColumn)
                && !string.IsNullOrEmpty(filterQuery)
                && PropertyValidator.IsValidProperty<TSource>(filterColumn))
            {
                source = source.Where(
                    $"{filterColumn}.Contains(@0)",
                    filterQuery);
            }

            var count = await source.CountAsync();

            if (!string.IsNullOrEmpty(sortColumn) && PropertyValidator.IsValidProperty<TSource>(sortColumn))
            {
                sortOrder = !string.IsNullOrEmpty(sortOrder) && sortOrder.ToUpper() == "ASC" ? "ASC" : "DESC";

                source = source.OrderBy(
                    $"{sortColumn} {sortOrder}"
                );
            }

            source = source
                .Skip(pageIndex * pageSize)
                .Take(pageSize);

            var dataResult = mapper.Map<List<TResponse>>(await source.ToListAsync());

            return new ApiResult<TSource, TResponse>(
                dataResult,
                count,
                pageIndex,
                pageSize,
                sortColumn,
                sortOrder,
                filterColumn,
                filterQuery);
        }

        #endregion

        #region Properties

        /// <summary>
        /// The data result
        /// </summary>
        public List<TResponse> DataResult { get; private set; }

        /// <summary>
        /// Zero-based index of current page.
        /// </summary>
        public int PageIndex { get; private set; }

        /// <summary>
        /// Number of items contained in each page.
        /// </summary>
        public int PageSize { get; private set; }

        /// <summary>
        /// Total items count
        /// </summary>
        public int TotalCount { get; private set; }

        /// <summary>
        /// Total pages count
        /// </summary>
        public int TotalPages { get; private set; }

        /// <summary>
        /// TRUE if the current page has a previous page, 
        /// FALSE otherwise.
        /// </summary>
        public bool HasPreviousPage
        {
            get { return (PageIndex > 0); }
        }

        /// <summary>
        /// TRUE if the current page has a next page, FALSE otherwise.
        /// </summary>
        public bool HasNextPage
        {
            get { return ((PageIndex + 1) < TotalPages); }
        }

        /// <summary>
        /// Sorting Column name (or null if none set)
        /// </summary>
        public string SortColumn { get; set; }

        /// <summary>
        /// Sorting Order ("ASC", "DESC" or null if none set)
        /// </summary>
        public string SortOrder { get; set; }

        /// <summary>
        /// Filter Column name (or null if none set)
        /// </summary>
        public string FilterColumn { get; set; }

        /// <summary>
        /// Filter Query string 
        /// (to be used within the given FilterColumn)
        /// </summary>
        public string FilterQuery { get; set; }

        #endregion
    }
}