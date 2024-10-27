using System.ComponentModel.DataAnnotations;

namespace RestfulApis_Application.Utilities
{
    public class PageResult<T>
    {
        public List<T> Results { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPages { get => (int)Math.Ceiling((decimal)TotalRecords / PageSize); }
    }
}
