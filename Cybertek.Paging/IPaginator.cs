namespace Cybertek.Paging
{
    public interface IPaginator
    {
        int PageNumber { get; set; }
        int Limit { get; set; }
        void SetPagingFromUri(Uri uri);
    }
}
