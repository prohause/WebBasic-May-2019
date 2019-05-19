using SIS.HTTP.Enums;
using SIS.HTTP.Requests.Contracts;
using SIS.HTTP.Responses.Contracts;
using SIS.WebServer.Result;

namespace SIS.Demo
{
    public class HomeController
    {
        public IHttpResponse Index(IHttpRequest request)
        {
            var content = "<h1>Hello, World!</h1>";
            return new HtmlResult(content, HttpResponseStatusCode.Ok);
        }
    }
}