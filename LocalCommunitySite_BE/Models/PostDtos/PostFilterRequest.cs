using LocalCommunitySite.Domain.Entities;
using System;

namespace LocalCommunitySite.API.Models.PostDtos
{
    public class PostFilterRequest
    {
        public string Title { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        
        public PostStatus? Status { get; set; }
    }
}
