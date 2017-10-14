using System;

namespace GranularMonitorSystem.Service.Exceptions
{
    public class ServiceAuthenticationException
    {
        public string Content{ get; }

        public ServiceAuthenticationException()
        {
        }

        public ServiceAuthenticationException(string content)
        {
            Content = content;
        }
    }
}
