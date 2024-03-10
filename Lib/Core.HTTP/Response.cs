using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.HTTP
{
    public class ResponseWrapper<SuccessReturn,ErrorReturn> where SuccessReturn:class where ErrorReturn:class
    {
        public SuccessReturn? Success { get; set; }
        public ErrorReturn? Error { get; set; }
        public bool IsSuccess { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
