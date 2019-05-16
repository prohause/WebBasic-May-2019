using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SoftUniHTTPHelper
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var tcpListener = new TcpListener(IPAddress.Loopback, 14444);

            tcpListener.Start();

            while (true)
            {
                var client = tcpListener.AcceptTcpClient();

                using (var stream = client.GetStream())
                {
                    var requestBytes = new byte[1000000];
                    var readBytes = stream.Read(requestBytes, 0, requestBytes.Length);
                    var result = Encoding.UTF8.GetString(requestBytes, 0, readBytes);

                    Console.WriteLine(new string('=', 80));
                    Console.WriteLine(result);

                    var sb = new StringBuilder();
                    var responseBody = "<form><input type='text' name='tweet' placeholder='Enter tweet...' /><input type='Submit' /></form>";
                    sb.AppendLine("HTTP/1.0 200 OK");
                    sb.AppendLine("Server: MyCustomServer/1.0");
                    sb.AppendLine($"Content-Length: {responseBody.Length}");
                    sb.AppendLine("Content-Type: text/html");
                    sb.AppendLine();
                    sb.AppendLine(responseBody);
                    var response = Encoding.UTF8.GetBytes(sb.ToString());
                    stream.Write(response, 0, response.Length);
                }
            }
        }
    }
}