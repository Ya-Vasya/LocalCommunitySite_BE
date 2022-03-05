using System.ComponentModel.DataAnnotations;

namespace LocalCommunitySite.API.Models.AuthenticationDtos
{
    public class UserRegistrationDto
    {
        public string Email { get; set; }
        
        public string Password { get; set; }
    }
}
