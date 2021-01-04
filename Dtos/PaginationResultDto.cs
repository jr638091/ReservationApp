using System.Collections.Generic;

namespace ReservationApp.Dtos
{
    public class PaginationQuery {
        public int Count { get; set; }
        public int PageIndex { get; set; }
    }
    public class PaginationResult<T>
    {
        public long Hits { get; set; }
        public long Count { get; set; }
        public long PageIndex { get; set; }
        public string NextPage { get; set; }
        public string PreviousPage { get; set; }
        public ICollection<T> Results { get; set; }

        public PaginationResult(ICollection<T> result, long hits, long count, long pageIndex, string nextPage, string previousPage)
        {
            this.Hits = hits;
            this.Count = count;
            this.Results = result;
            PageIndex = pageIndex;
            NextPage = nextPage;
            PreviousPage = previousPage;
        }
    }
}