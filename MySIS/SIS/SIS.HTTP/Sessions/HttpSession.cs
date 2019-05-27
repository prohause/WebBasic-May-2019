using System.Collections.Generic;
using SIS.HTTP.Common;
using SIS.HTTP.Sessions.Contracts;

namespace SIS.HTTP.Sessions
{
    public class HttpSession : IHttpSession
    {
        private readonly Dictionary<string, object> _sessionParameters;

        public HttpSession(string id)
        {
            Id = id;
            _sessionParameters = new Dictionary<string, object>();
        }

        public string Id { get; }

        public object GetParameter(string parameterName)
        {
            CoreValidator.ThrowIfNullOrEmpty(parameterName, nameof(parameterName));

            return _sessionParameters[parameterName];
        }

        public bool ContainsParameter(string parameterName)
        {
            CoreValidator.ThrowIfNullOrEmpty(parameterName, nameof(parameterName));

            return _sessionParameters.ContainsKey(parameterName);
        }

        public void AddParameter(string parameterName, object parameter)
        {
            CoreValidator.ThrowIfNullOrEmpty(parameterName, nameof(parameterName));
            CoreValidator.ThrowIfNull(parameter, nameof(parameter));

            _sessionParameters[parameterName] = parameter;
        }

        public void ClearParameters()
        {
            _sessionParameters.Clear();
        }
    }
}