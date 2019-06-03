﻿using System.Collections.Concurrent;
using SIS.HTTP.Sessions;
using SIS.HTTP.Sessions.Contracts;

namespace SIS.WebServer.Sessions
{
    public class HttpSessionStorage
    {
        public const string SessionCookieKey = "SIS_ID";

        private static readonly ConcurrentDictionary<string, IHttpSession> HttpSessions = new ConcurrentDictionary<string, IHttpSession>();

        public static IHttpSession GetSession(string id)
        {
            return HttpSessions.GetOrAdd(id, _ => new HttpSession(id));
        }
    }
}