using LocalCommunitySite.Domain.Entities;
using System;

namespace LocalCommunitySite.API.Models.PostDtos
{
    public class PostGetDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public PostStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
