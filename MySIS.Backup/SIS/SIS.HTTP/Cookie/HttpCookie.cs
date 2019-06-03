using SIS.HTTP.Common;
using System;
using System.Text;

namespace SIS.HTTP.Cookie
{
    public class HttpCookie
    {
        private const int HttpCookieDefaultExpirationDays = 3;

        private const string HttpCookieDefaultPath = "/";

        public HttpCookie(string key, string value, int httpCookieDefaultExpirationDays = HttpCookieDefaultExpirationDays,
            string path = HttpCookieDefaultPath)
        {
            CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));
            CoreValidator.ThrowIfNullOrEmpty(value, nameof(value));

            Key = key;
            Value = value;
            ExpirationDate = DateTime.UtcNow.AddDays(httpCookieDefaultExpirationDays);
            Path = path;
            IsNew = true;
        }

        public HttpCookie(string key, string value, bool isNew, int httpCookieDefaultExpirationDays = HttpCookieDefaultExpirationDays,
            string path = HttpCookieDefaultPath) : this(key, value, httpCookieDefaultExpirationDays, path)
        {
            IsNew = isNew;
        }

        public string Key { get; }

        public string Value { get; }

        public DateTime ExpirationDate { get; private set; }

        public string Path { get; }

        public bool IsNew { get; }

        public bool HttpOnly { get; set; } = true;

        public void Delete()
        {
            ExpirationDate = DateTime.UtcNow.AddDays(-1);
        }

        public override string ToString()
        {
            var result = new StringBuilder();

            result.Append($"{Key}={Value}; Expires={ExpirationDate:R}");

            if (HttpOnly)
            {
                result.Append($"; HttpOnly");
            }

            result.Append($"; Path={Path}");

            return result.ToString();
        }
    }
}