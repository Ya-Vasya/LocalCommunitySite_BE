using LocalCommunitySite.API.Models.Shared;
using LocalCommunitySite.Domain.Entities;
using System;

namespace LocalCommunitySite.API.Models.PostDtos
{
    public class PostQueryDto : BaseQuery
    {
        public PostStatus? Status { get; set; }

        public PostSection? Section { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
