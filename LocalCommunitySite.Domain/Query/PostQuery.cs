using LocalCommunitySite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalCommunitySite.Domain.Query
{
    public class PostQuery : BaseQuery
    {
        public PostStatus? Status { get; set; }

        public PostSection? Section { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
