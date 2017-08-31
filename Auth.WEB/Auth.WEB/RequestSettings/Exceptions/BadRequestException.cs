namespace Auth.WEB.RequestSettings.Exceptions
{
    internal class BadRequestException : WebRequestException
    {
        public BadRequestException(string url) : base("404. Bad request", url)
        {
        }
    }
}