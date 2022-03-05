using System;
using System.Globalization;
using System.Net;

namespace LocalCommunitySite.API.Extentions.Exceptions
{
    public class CustomException : Exception
    {
        public virtual HttpStatusCode StatusCode { get { return HttpStatusCode.InternalServerError; } }

        public CustomException() : base() { }

        public CustomException(string message) : base(message) { }

        public CustomException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
