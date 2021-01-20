using System.Collections.Generic;

namespace Xefiros.Shared.Dtos
{
    public class ApiResponseDto<T>
    {
        public List<T> DataResult { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }

        public bool HasPreviousPage
        {
            get { return (PageIndex > 0); }
        }

        public bool HasNextPage
        {
            get { return ((PageIndex + 1) < TotalPages); }
        }

        public string FilterColumn { get; set; }
        public string FilterQuery { get; set; }
    }
}