using System;

namespace Auth.WEB.RequestSettings.Exceptions
{
    internal class WebRequestException : Exception
    {
        public WebRequestException(string message, string url) : base(message)
        {
            Url = url;
        }

        public string Url { get; set; }
    }
}