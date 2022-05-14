using System.Globalization;
using System.Net;

namespace LocalCommunitySite.API.Extentions.Exceptions
{
    public class NotFoundException : CustomException
    {
        public override HttpStatusCode StatusCode { get { return HttpStatusCode.NotFound; } }

        public NotFoundException() : base() { }

        public NotFoundException(string message) : base(message) { }

        public NotFoundException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
