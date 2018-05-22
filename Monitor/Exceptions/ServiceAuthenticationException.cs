using System;
using System.Threading.Tasks;

namespace Monitor.Exceptions
{
    public class ServiceAuthenticationException : Exception 
    {
        private readonly IDialogService DialogService;

        public string Content { get; }

        public ServiceAuthenticationException()
        {
        }

        public ServiceAuthenticationException(string content)
        {
            Content = content;
        }
    }
}
