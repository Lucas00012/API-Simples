using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Core.Helpers.Exceptions.Base
{
    public abstract class BaseHttpException : Exception
    {
        public abstract HttpStatusCode StatusCode { get; }

        public BaseHttpException(string message) : base(message)
        {

        }
    }
}
