using API.Core.Helpers.Exceptions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Core.Helpers.Exceptions
{
    public class NotFoundException : BaseHttpException
    {
        public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;

        public NotFoundException() : base("Entidade não encontrada")
        {

        }
    }
}
