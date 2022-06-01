using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalCommunitySite.Domain.Shared
{
    public class Pagination<T>
    {
        public int TotalLength { get; set; }

        public IEnumerable<T> List { get; set; }
    }
}
