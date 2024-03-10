using Core.HTTP;
using Core.HTTP.Interfaces;
using Core.HTTP.RequestContentBuilders;
using Core.HTTP.ResponseDeserializers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTest.Requests;
class GeneralTestRequest<RequestContent, SuccessReturn> : Request<RequestContent, SuccessReturn, ProblemDetails> where SuccessReturn : class where RequestContent : class
{
    public override IResponseDeserializer<SuccessReturn, ProblemDetails> ResponseDeserializer { get; set; } = new JsonResponseDeserializer<SuccessReturn, ProblemDetails>();
    public override IRequestContentBuilder<RequestContent>? HTTPContentBuilder { get; set; } = new RequestJsonContentBuilder<RequestContent>();

}

