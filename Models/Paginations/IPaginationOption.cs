namespace Chatik.Models.Paginations
{
    public interface IPaginationOption
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}
