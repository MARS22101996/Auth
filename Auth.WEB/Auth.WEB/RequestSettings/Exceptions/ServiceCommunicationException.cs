using System;

namespace Auth.WEB.RequestSettings.Exceptions
{
    public class ServiceCommunicationException : Exception
    {
        public ServiceCommunicationException(string message) : base(message)
        {
        }
    }
}