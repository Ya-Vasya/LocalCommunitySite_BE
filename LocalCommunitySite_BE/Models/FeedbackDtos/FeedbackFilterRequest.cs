using System;

namespace LocalCommunitySite.API.Models.FeedbackDtos
{
    public class FeedbackFilterRequest
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
