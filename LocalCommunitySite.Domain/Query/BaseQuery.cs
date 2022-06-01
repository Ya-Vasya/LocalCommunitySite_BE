using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalCommunitySite.Domain.Query
{
    public class BaseQuery
    {
        public int Offset { get; set; }

        public int Limit { get; set; }
    }
}
