namespace LocalCommunitySite.API.Models.CommentDtos
{
    public class CommentDto
    {
        public string Text { get; set; }

        public int PostId { get; set; }

        public int? ParentCommentId { get; set; }
    }
}
