using System.Collections.Generic;

namespace ReservationApp.Dtos {
    public class PaginationResult<T> {
        public long Hits { get; set; }
        public long Count { get; set; }
        public long initial { get; set; }
        public ICollection<T> Results { get; set; }

        public PaginationResult(ICollection<T> result, long hits, long count, long initial)
        {
            this.Hits = hits;
            this.Count = count;
            this.Results = result;
            this.initial = initial;
        }
    }
}