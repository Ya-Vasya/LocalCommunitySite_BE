namespace LocalCommunitySite.API.Models.UserDtos
{
    public class UserEditDto
    {
        public string Email { get; set; }

        public string CurrentPassword { get; set; }

        public string NewPassword { get; set; }
    }
}