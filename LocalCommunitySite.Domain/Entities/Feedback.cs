using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalCommunitySite.Domain.Entities
{
    public class Feedback
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
