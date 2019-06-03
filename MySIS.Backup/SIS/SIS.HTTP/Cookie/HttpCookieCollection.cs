using SIS.HTTP.Common;
using SIS.HTTP.Cookie.Contracts;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SIS.HTTP.Cookie
{
    public class HttpCookieCollection : IHttpCookieCollection
    {
        private Dictionary<string, HttpCookie> _cookies;

        public HttpCookieCollection()
        {
            _cookies = new Dictionary<string, HttpCookie>();
        }

        public IEnumerator<HttpCookie> GetEnumerator()
        {
            return _cookies.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void AddCookie(HttpCookie cookie)
        {
            CoreValidator.ThrowIfNull(cookie, nameof(cookie));

            _cookies.Add(cookie.Key, cookie);
        }

        public bool ContainsCookie(string key)
        {
            CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));

            return _cookies.ContainsKey(key);
        }

        public HttpCookie GetCookie(string key)
        {
            CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));

            return _cookies[key];
        }

        public bool HasCookies()
        {
            return _cookies.Count >= 1;
        }

        public override string ToString()
        {
            var result = new StringBuilder();

            foreach (var cookie in this._cookies.Values)
            {
                result.Append($"{GlobalConstants.HttpCookieStringSeparator} {cookie}").Append(GlobalConstants.HttpNewLine);
            }

            return result.ToString();
        }
    }
}