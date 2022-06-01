using System.Collections.Generic;

namespace LocalCommunitySite.API.Models.Shared
{
    public class PaginationDto<T>
    {
        public int TotalLength { get; set; }

        public IEnumerable<T> List { get; set; }
    }
}
