using Demo.App.Controllers;
using SIS.HTTP.Enums;
using SIS.WebServer;
using SIS.WebServer.Routing;
using SIS.WebServer.Routing.Contracts;

namespace Demo.App
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            IServerRoutingTable serverRoutingTable = new ServerRoutingTable();

            serverRoutingTable.Add(HttpRequestMethod.Get, "/", httpRequest
                => new HomeController().Home(httpRequest));

            serverRoutingTable.Add(HttpRequestMethod.Get, "/login", httpRequest
                => new HomeController().Login(httpRequest));
            serverRoutingTable.Add(HttpRequestMethod.Get, "/logout", httpRequest
                => new HomeController().Logout(httpRequest));

            Server server = new Server(80, serverRoutingTable);
            server.Run();
        }
    }
}