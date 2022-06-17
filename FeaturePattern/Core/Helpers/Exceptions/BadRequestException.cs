using API.Core.Helpers.Exceptions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Core.Helpers.Exceptions
{
    public class BadRequestException : BaseHttpException
    {
        public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public BadRequestException() : base("Erro inesperado. Tente novamente mais tarde")
        {

        }

        public BadRequestException(string message) : base(message)
        {

        }
    }
}
