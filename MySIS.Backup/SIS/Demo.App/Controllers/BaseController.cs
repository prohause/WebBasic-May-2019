using SIS.HTTP.Responses.Contracts;
using SIS.WebServer.Result;
using System.IO;
using System.Runtime.CompilerServices;
using SIS.HTTP.Cookie;
using SIS.HTTP.Enums;
using SIS.HTTP.Requests.Contracts;

namespace Demo.App.Controllers
{
    public abstract class BaseController
    {
        protected IHttpRequest HttpRequest { get; set; }

        private bool IsLoggedIn()
        {
            return HttpRequest.HttpSession.ContainsParameter("username");
        }

        public IHttpResponse View([CallerMemberName] string view = null)
        {
            var controllerName = this.GetType().Name.Replace("Controller", string.Empty);
            var viewName = view;

            var viewContent = File.ReadAllText("Views/" + controllerName + "/" + viewName + ".html");

            viewContent = ParseTemplate(viewContent);

            var htmlResult = new HtmlResult(viewContent, HttpResponseStatusCode.Ok);
            htmlResult.AddCookie(new HttpCookie("lang", "en"));

            return htmlResult;
        }

        private string ParseTemplate(string viewContent)
        {
            return viewContent.Replace("@Model.HelloMessage", IsLoggedIn() ? $"Hello, {HttpRequest.HttpSession.GetParameter("username")}" : "Hello World from SIS WebServer");
        }

        public IHttpResponse Redirect(string url)
        {
            return new RedirectResult(url);
        }
    }
}