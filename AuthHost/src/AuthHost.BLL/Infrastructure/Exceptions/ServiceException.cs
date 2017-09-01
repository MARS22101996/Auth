using System;

namespace AuthHost.BLL.Infrastructure.Exceptions
{
    public class ServiceException : Exception
    {
        public string Target { get; protected set; }

        public ServiceException(string message, string target) : base(message)
        {
            Target = target;
        }
    }
}