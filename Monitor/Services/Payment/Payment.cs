using System;
using Monitor.Services.Requests;

namespace Monitor.Payment
{
    public class Payment
    {
        private readonly IRequests _request;

        public Payment(IRequests request)
        {
            _request = request;
        }
    }
}
