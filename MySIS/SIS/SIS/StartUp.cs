using SIS.HTTP.Enums;
using SIS.HTTP.Extensions;
using SIS.HTTP.Requests;
using System;
using System.Globalization;
using System.Text;
using SIS.HTTP.Headers;
using SIS.HTTP.Responses;

namespace SIS
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //var request =
            //    "POST /cgi-bin/process.cgi?id=Gosho HTTP/1.1\r\n" +
            //    "User-Agent: Mozilla/4.0 (compatible; MSIE5.01; Windows NT)\r\n" +
            //    "Host: www.tutorialspoint.com\r\n" +
            //    "Content-Type: application/x-www-form-urlencoded\r\n" +
            //    "Content-Length: length\r\n" +
            //    "Accept-Language: en-us\r\n" +
            //    "Accept-Encoding: gzip, deflate\r\n" +
            //    "Connection: Keep-Alive\r\n" +
            //    "\r\n" +
            //    "licenseID=string&content=string&/paramsXML=string";

            //var httpRequest = new HttpRequest(request);

            //var statusCode = HttpResponseStatusCode.BadRequest;
            //Console.WriteLine(statusCode.GetStatusLine());

            var response = new HttpResponse(HttpResponseStatusCode.InternalServerError);
            response.AddHeader(new HttpHeader("Host", "localhost:5000"));
            response.AddHeader(new HttpHeader("Date", DateTime.Now.ToString(CultureInfo.InvariantCulture)));

            response.Content = Encoding.UTF8.GetBytes("<h1>Hello World!</h1>");

            Console.WriteLine(Encoding.UTF8.GetString(response.GetBytes()));
        }
    }
}