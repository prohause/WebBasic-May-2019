using SIS.HTTP.Common;
using SIS.HTTP.Enums;
using SIS.HTTP.Exceptions;
using SIS.HTTP.Headers;
using SIS.HTTP.Requests.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SIS.HTTP.Requests
{
    public class HttpRequest : IHttpRequest
    {
        public HttpRequest(string requestString)
        {
            CoreValidator.ThrowIfNullOrEmpty(requestString, nameof(requestString));
            FormData = new Dictionary<string, object>();
            QueryData = new Dictionary<string, object>();
            Headers = new HttpHeaderCollection();

            ParseRequest(requestString);
        }

        private void ParseRequestQueryDataParameters()
        {
            if (!Url.Contains('?'))
            {
                return;
            }

            Url.Split('?')[1]
                .Split('#')[0]
                .Split('&')
                .Select(queryParameter => queryParameter.Split('='))
                .ToList()
                .ForEach(queryParamKeyValuePair => QueryData[queryParamKeyValuePair[0]] = queryParamKeyValuePair[1]);
        }

        private void ParseRequestFormDataParameters(string requestBody)
        {
            if (string.IsNullOrEmpty(requestBody))
            {
                return;
            }
            //TODO: Parse Multiple Parameters By Name
            requestBody.Split('&')
                .Select(formParameters => formParameters.Split('='))
                .ToList()
                .ForEach(formParamKeyValuePair => FormData[formParamKeyValuePair[0]] = formParamKeyValuePair[1]);
        }

        private void ParseRequestParameters(string requestBody)
        {
            ParseRequestQueryDataParameters();
            ParseRequestFormDataParameters(requestBody);
        }

        private void ParseRequestHeaders(IEnumerable<string> headersParams)
        {
            headersParams.Select(unparsedHeader => unparsedHeader.Split(new[] { ' ', ':' }, StringSplitOptions.RemoveEmptyEntries))
                .ToList()
                .ForEach(headerKeyValuePair => Headers.AddHeader(new HttpHeader(headerKeyValuePair[0], headerKeyValuePair[1])));
        }

        private void ParseRequestPath()
        {
            Path = Url.Split('?')[0];
        }

        private void ParseRequestUrl(IReadOnlyList<string> requestLineParams)
        {
            Url = requestLineParams[1];
        }

        private void ParseRequestMethod(IReadOnlyList<string> requestLineParams)
        {
            var parseResult = Enum.TryParse(requestLineParams[0], true, out HttpRequestMethod method);
            if (!parseResult)
            {
                throw new BadRequestException(string.Format(GlobalConstants.UnsupportedHttpMethodExceptionMessage, requestLineParams[0]));
            }

            RequestMethod = method;
        }

        private static bool IsValidRequestLine(IReadOnlyList<string> requestLineParams)
        {
            return requestLineParams.Count == 3 && requestLineParams[2] == GlobalConstants.HttpOneProtocolFragment;
        }

        private void ParseRequest(string requestString)
        {
            var splitRequestContent = requestString
                .Split(new[] { GlobalConstants.HttpNewLine }, StringSplitOptions.None);

            var requestLineParams = splitRequestContent[0].Trim()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (Equals(!IsValidRequestLine(requestLineParams)))
            {
                throw new BadRequestException();
            }

            ParseRequestMethod(requestLineParams);
            ParseRequestUrl(requestLineParams);
            ParseRequestPath();

            ParseRequestHeaders(splitRequestContent.Skip(1).TakeWhile(line => line != string.Empty).ToArray());
            //ParseCookies();

            ParseRequestParameters(splitRequestContent[splitRequestContent.Length - 1]);
        }

        public string Path { get; private set; }
        public string Url { get; private set; }
        public Dictionary<string, object> FormData { get; }
        public Dictionary<string, object> QueryData { get; }
        public IHttpHeaderCollection Headers { get; }
        public HttpRequestMethod RequestMethod { get; private set; }
    }
}