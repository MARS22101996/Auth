using System;

namespace AuthClient.RequestSettings.Exceptions
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