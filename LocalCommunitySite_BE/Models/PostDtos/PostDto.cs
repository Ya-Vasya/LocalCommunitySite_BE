using LocalCommunitySite.Domain.Entities;
using System;

namespace LocalCommunitySite.API.Models.PostDtos
{
    public class PostDto
    {
        public string Title { get; set; }

        public string Image { get; set; }

        public string Body { get; set; }

        public DateTime CreatedAt { get; set; }

        public PostStatus Status { get; set; }

        public PostSection Section { get; set; }
    }
}
