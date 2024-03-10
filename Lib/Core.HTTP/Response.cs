using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.HTTP
{
    public class ResponseWrapper<ReturnType> where ReturnType:class
    {
        public ReturnType? Response { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
