namespace Chatik.Models.Paginations
{
    public class PaginationOption: IPaginationOption
    {
        public int CurrentPage { get;  set; }
        public int PageSize { get;  set; }
    }
}
