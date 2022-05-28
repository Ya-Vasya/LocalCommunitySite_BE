using System;

namespace LocalCommunitySite.API.Models.FeedbackDtos
{
    public class FeedbackDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
