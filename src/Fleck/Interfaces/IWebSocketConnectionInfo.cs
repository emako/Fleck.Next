using System;
using System.Collections.Generic;

namespace Fleck
{
    public interface IWebSocketConnectionInfo
    {
        public string SubProtocol { get; }

        public string Origin { get; }

        public string Host { get; }

        public string Path { get; }

        public string ClientIpAddress { get; }

        public int ClientPort { get; }

        public IDictionary<string, string> Cookies { get; }

        public IDictionary<string, string> Headers { get; }

        public Guid Id { get; }

        public string NegotiatedSubProtocol { get; }
    }
}
