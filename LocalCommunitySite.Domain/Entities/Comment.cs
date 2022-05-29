using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalCommunitySite.Domain.Entities
{
    public class Comment
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public bool IsEdited { get; set; }

        public bool IsDeleted { get; set; }

        public int PostId { get; set; }

        public Post Post { get; set; }

        public int? ParentCommentId { get; set; }

        public Comment ParentComment { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public ICollection<Comment> Replies { get; set; }
    }
}
