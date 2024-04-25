using Cybertek.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;

namespace Cybertek.Paging
{
    public class Paginator : IPaginator
    {
        private int _pageNumber;
        private int _limit;
        public const string PageNumberKey = "pageNumber";
        private const string LimitKey = "limit";
        private const int DefaultPageNumber = 1;
        private const int DefaultLimit = 5;
        
        public int PageNumber { get; set; }
        public int Limit { get; set; }
        public Uri ActionUri { get; set; }

        private Paginator(int pageNumber, int limit)
        {
            _pageNumber = pageNumber;
            _limit = limit;
        }
        public Paginator() : this(DefaultPageNumber, DefaultLimit)
        {
        }
        
        public Paginator(Uri uri) : this()
        {
            SetPagingFromUri(uri);
        }

        public void SetPagingFromUri(Uri uri)
        {
            ActionUri = uri;
            Dictionary<string, StringValues> collection = QueryHelpers.ParseQuery(uri.Query);

            PageNumber = collection.ContainsKey(PageNumberKey) ? int.Parse(collection[PageNumberKey].First()) : DefaultPageNumber;

            Limit = collection.ContainsKey(LimitKey) ? int.Parse(collection[LimitKey].First()) : DefaultLimit;
        }

        public static Paginator FromRequest(HttpRequest request)
        {
            return new Paginator(request.GetFullDisplayUri());
        }
    }
}
