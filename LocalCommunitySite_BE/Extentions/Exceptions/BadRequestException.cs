using System.Globalization;
using System.Net;

namespace LocalCommunitySite.API.Extentions.Exceptions
{
    public class BadRequestException : CustomException
    {
        public override HttpStatusCode StatusCode { get { return HttpStatusCode.BadRequest; } }

        public BadRequestException() : base() { }

        public BadRequestException(string message) : base(message) { }

        public BadRequestException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
