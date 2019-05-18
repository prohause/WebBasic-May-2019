using SIS.HTTP.Enums;
using System.Collections.Generic;
using System.Linq;

namespace SIS.HTTP.Extensions
{
    public static class HttpResponseStatusExtensions
    {
        public static string GetStatusLine(this HttpResponseStatusCode statusCode)
        {
            var statusLine = statusCode.ToString().ToList();
            var result = new List<char>();

            for (int i = 0; i < statusLine.Count; i++)
            {
                if (i > 0 && char.IsUpper(statusLine[i]))
                {
                    result.Add(' ');
                    result.Add(statusLine[i]);
                }
                else
                {
                    result.Add(statusLine[i]);
                }
            }

            var statusNumber = (int)statusCode;

            return statusNumber + " " + string.Join("", result);
        }
    }
}