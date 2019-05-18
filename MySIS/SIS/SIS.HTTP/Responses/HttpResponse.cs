using System;
using SIS.HTTP.Common;
using SIS.HTTP.Enums;
using SIS.HTTP.Extensions;
using SIS.HTTP.Headers;
using SIS.HTTP.Responses.Contracts;
using System.Text;

namespace SIS.HTTP.Responses
{
    public class HttpResponse : IHttpResponse
    {
        public HttpResponse()
        {
            Headers = new HttpHeaderCollection();
            Content = new byte[0];
        }

        public HttpResponse(HttpResponseStatusCode statusCode) : this()
        {
            CoreValidator.ThrowIfNull(statusCode, nameof(statusCode));
            StatusCode = statusCode;
        }

        public HttpResponseStatusCode StatusCode { get; set; }
        public IHttpHeaderCollection Headers { get; }
        public byte[] Content { get; set; }

        public void AddHeader(HttpHeader header)
        {
            Headers.AddHeader(header);
        }

        public byte[] GetBytes()
        {
            byte[] httpResponseBytesWithoutBody = Encoding.UTF8.GetBytes(this.ToString());

            byte[] httpResponseWithBody = new byte[httpResponseBytesWithoutBody.Length + Content.Length];
            Array.Copy(httpResponseBytesWithoutBody, httpResponseWithBody, httpResponseBytesWithoutBody.Length);
            Array.Copy(Content, 0, httpResponseWithBody, httpResponseBytesWithoutBody.Length, Content.Length);

            return httpResponseWithBody;
        }

        public override string ToString()
        {
            var result = new StringBuilder();
            result.Append($"{GlobalConstants.HttpOneProtocolFragment} {StatusCode.GetStatusLine()}")
                .Append(GlobalConstants.HttpNewLine)
                .Append(Headers).Append(GlobalConstants.HttpNewLine);

            result.Append(GlobalConstants.HttpNewLine);

            return result.ToString();
        }
    }
}