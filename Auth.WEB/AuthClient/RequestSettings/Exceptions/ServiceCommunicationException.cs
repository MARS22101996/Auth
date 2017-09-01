using System;

namespace AuthClient.RequestSettings.Exceptions
{
    public class ServiceCommunicationException : Exception
    {
        public ServiceCommunicationException(string message) : base(message)
        {
        }
    }
}