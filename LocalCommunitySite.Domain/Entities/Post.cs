﻿using System;

namespace LocalCommunitySite.Domain.Entities
{
    public class Post
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Image { get; set; }

        public string Body { get; set; }

        public DateTime CreatedAt { get; set; }

        public PostStatus Status { get; set; }

        public PostSection Section { get; set; }
    }
}
