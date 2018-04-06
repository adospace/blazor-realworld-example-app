using RealWorld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealWorld.Services
{
    public class ApiServiceException : Exception
    {
        public Error Error { get; }

        public ApiServiceException(Error error = null)
        {
            Error = error;
        }
    }
}
