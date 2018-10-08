using System;
using System.Threading.Tasks;

namespace Monitor.Exceptions
{
    public class ServiceAuthenticationException : Exception 
    {
        public System.Net.HttpStatusCode HttpCode { get; }

        public string Content { get; }

        public ServiceAuthenticationException()
        {
        }

        public ServiceAuthenticationException(System.Net.HttpStatusCode code, string content)
        {
            Content = content;
            HttpCode = code;
        }
    }
}
