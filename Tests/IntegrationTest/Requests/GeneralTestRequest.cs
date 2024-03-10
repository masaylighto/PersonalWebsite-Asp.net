using Core.HTTP;
using Core.HTTP.Interfaces;
using Core.HTTP.RequestContentBuilders;
using Core.HTTP.ResponseDeserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTest.Requests;
class GeneralTestRequest<RequestContent,ReturnType> : Request<RequestContent,ReturnType> where ReturnType : class where RequestContent : class
{
    public override IResponseDeserializer<ReturnType> ResponseDeserializer { get; set; } = new JsonResponseDeserializer<ReturnType>();
    public override IRequestContentBuilder<RequestContent>? HTTPContentBuilder { get; set; } = new RequestJsonContentBuilder<RequestContent>();

}

