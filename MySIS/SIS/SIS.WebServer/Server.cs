using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using SIS.WebServer.Routing.Contracts;

namespace SIS.WebServer
{
    public class Server
    {
        private const string LocalhostIpAddress = "127.0.0.1";

        private readonly int port;

        private readonly TcpListener listener;

        private readonly IServerRoutingTable serverRoutingTable;

        private bool isRunning;

        public Server(int port, IServerRoutingTable serverRoutingTable)
        {
            this.port = port;
            this.serverRoutingTable = serverRoutingTable;

            listener = new TcpListener(IPAddress.Parse(LocalhostIpAddress), port);

            while (this.isRunning)
            {
                Console.WriteLine("Waiting for client...");

                var client = listener.AcceptSocket();

                Listen(client);
            }
        }

        public void Run()
        {
            listener.Start();
            isRunning = true;

            Console.WriteLine($"Server started on http://{LocalhostIpAddress}:{port}");

            while (this.isRunning)
            {
                Console.WriteLine("Waiting for client...");
                var client = listener.AcceptSocket();
                Listen(client);
            }
        }

        public void Listen(Socket client)
        {
            var connectionHandler = new ConnectionHandler(client, serverRoutingTable);
            connectionHandler.ProcessRequests();
        }
    }
}