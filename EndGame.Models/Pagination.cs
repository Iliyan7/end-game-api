using System.Collections.Generic;

namespace EndGame.Models
{
    public class Pagination<T> where T : class
    {
        public int PageIndex { get; set; }

        public int TotalPages { get; set; }

        public IEnumerable<T> Result { get; set; }
    }
}
