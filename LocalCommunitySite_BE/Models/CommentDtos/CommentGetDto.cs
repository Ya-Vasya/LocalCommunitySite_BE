using System;
using System.Collections.Generic;

namespace LocalCommunitySite.API.Models.CommentDtos
{
    public class CommentGetDto
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public bool IsEdited { get; set; }

        public bool IsDeleted { get; set; }

        public int PostId { get; set; }

        public int? ParentCommentId { get; set; }

        public string UserId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public ICollection<CommentGetDto> Replies { get; set; }
    }
}
