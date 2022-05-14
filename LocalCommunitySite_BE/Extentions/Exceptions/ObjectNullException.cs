using System.Globalization;
using System.Net;

namespace LocalCommunitySite.API.Extentions.Exceptions
{
    public class ObjectNullException : CustomException
    {
        public override HttpStatusCode StatusCode { get { return HttpStatusCode.NotAcceptable; } }

        public ObjectNullException() : base() { }

        public ObjectNullException(string message) : base(message) { }

        public ObjectNullException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
