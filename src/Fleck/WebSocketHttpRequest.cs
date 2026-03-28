using System.Collections.Generic;
using System;

namespace Fleck
{
    public class WebSocketHttpRequest
    {
        private readonly IDictionary<string, string> _headers = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

        public string Method { get; set; }

        public string Path { get; set; }

        public string Body { get; set; }

        public string Scheme { get; set; }

        public byte[] Bytes { get; set; }

        public string this[string name]
            => _headers.TryGetValue(name, out string value) ? value : default;

        public IDictionary<string, string> Headers => _headers;

        public string[] SubProtocols => _headers.TryGetValue("Sec-WebSocket-Protocol", out string value)
            ? value.Split([',', ' '], StringSplitOptions.RemoveEmptyEntries)
            : [];
    }
}
