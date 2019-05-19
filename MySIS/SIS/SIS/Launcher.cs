using System;
using SIS.HTTP.Enums;
using SIS.HTTP.Requests.Contracts;
using SIS.HTTP.Responses.Contracts;
using SIS.WebServer;
using SIS.WebServer.Routing;

namespace SIS.Demo
{
    public class Launcher
    {
        public static void Main(string[] args)
        {
            var serverRoutingTable = new ServerRoutingTable();

            serverRoutingTable.Add(HttpRequestMethod.Get, "/", request => new HomeController().Index(request));

            var server = new Server(8000, serverRoutingTable);
            server.Run();
        }
    }
}