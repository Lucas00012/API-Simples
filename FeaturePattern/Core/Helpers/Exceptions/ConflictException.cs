using API.Core.Helpers.Exceptions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Core.Helpers.Exceptions
{
    public class ConflictException : BaseHttpException
    {
        public override HttpStatusCode StatusCode => HttpStatusCode.Conflict;

        public ConflictException() : base("Dados já existentes no sistema")
        {

        }

        public ConflictException(string message) : base(message)
        {

        }
    }
}
