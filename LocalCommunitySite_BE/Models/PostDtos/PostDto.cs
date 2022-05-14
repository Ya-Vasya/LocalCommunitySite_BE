using System;

namespace LocalCommunitySite.API.Models.PostDtos
{
    public class PostDto
    {
        public string Title { get; set; }

        public string Body { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
