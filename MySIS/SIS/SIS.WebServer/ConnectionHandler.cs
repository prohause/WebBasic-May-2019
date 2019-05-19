using System;
using System.Net.Sockets;
using System.Text;
using SIS.HTTP.Common;
using SIS.HTTP.Enums;
using SIS.HTTP.Exceptions;
using SIS.HTTP.Requests;
using SIS.HTTP.Requests.Contracts;
using SIS.HTTP.Responses;
using SIS.HTTP.Responses.Contracts;
using SIS.WebServer.Result;
using SIS.WebServer.Routing.Contracts;

namespace SIS.WebServer
{
    public class ConnectionHandler
    {
        private readonly Socket client;

        private readonly IServerRoutingTable serverRoutingTable;

        public ConnectionHandler(Socket client, IServerRoutingTable serverRoutingTable)
        {
            CoreValidator.ThrowIfNull(client, nameof(client));
            CoreValidator.ThrowIfNull(serverRoutingTable, nameof(serverRoutingTable));

            this.client = client;
            this.serverRoutingTable = serverRoutingTable;
        }

        public void ProcessRequests()
        {
            try
            {
                var httpRequest = this.ReadRequest();

                if (httpRequest != null)
                {
                    Console.WriteLine($"Processing: {httpRequest.RequestMethod} {httpRequest.Path}...");
                    var httpResponse = this.HandleRequest(httpRequest);
                    this.PrepareResponse(httpResponse);
                }
            }
            catch (BadRequestException e)
            {
                PrepareResponse(new TextResult(e.ToString(), HttpResponseStatusCode.BadRequest));
            }
            catch (Exception e)
            {
                PrepareResponse(new TextResult(e.ToString(), HttpResponseStatusCode.InternalServerError));
            }
            client.Shutdown(SocketShutdown.Both);
        }

        private void PrepareResponse(IHttpResponse httpResponse)
        {
            var byteSegment = httpResponse.GetBytes();
            client.Send(byteSegment, SocketFlags.None);
        }

        private IHttpResponse HandleRequest(IHttpRequest httpRequest)
        {
            if (Equals(!serverRoutingTable.Contains(httpRequest.RequestMethod, httpRequest.Path)))
            {
                return new TextResult($"Route with method {httpRequest.RequestMethod} and path \"{httpRequest.Path}\" not found.", HttpResponseStatusCode.NotFound);
            }

            return serverRoutingTable.Get(httpRequest.RequestMethod, httpRequest.Path).Invoke(httpRequest);
        }

        private IHttpRequest ReadRequest()
        {
            var result = new StringBuilder();
            var data = new ArraySegment<byte>(new byte[1024]);

            while (true)
            {
                var numberOfBytesRead = client.Receive(data.Array, SocketFlags.None);

                if (numberOfBytesRead == 0)
                {
                    break;
                }

                var bytesAsString = Encoding.UTF8.GetString(data.Array, 0, numberOfBytesRead);
                result.Append(bytesAsString);

                if (numberOfBytesRead < 1023)
                {
                    break;
                }
            }

            return result.Length == 0 ? null : new HttpRequest(result.ToString());
        }
    }
}