using System;
using Fleck.Handlers;

namespace Fleck
{
    public class HandlerFactory
    {
        public static IHandler BuildHandler(WebSocketHttpRequest request, Action<string> onMessage, Action onClose, Action<byte[]> onBinary, Action<byte[]> onPing, Action<byte[]> onPong)
        {
            var version = GetVersion(request);

            return version switch
            {
                "76" => Draft76Handler.Create(request, onMessage),
                "7" or "8" or "13" => Hybi13Handler.Create(request, onMessage, onClose, onBinary, onPing, onPong),
                "policy-file-request" => FlashSocketPolicyRequestHandler.Create(request),
                _ => throw new WebSocketException(WebSocketStatusCodes.UnsupportedDataType),
            };
        }

        public static string GetVersion(WebSocketHttpRequest request)
        {
            string version;
            if (request.Headers.TryGetValue("Sec-WebSocket-Version", out version))
                return version;

            if (request.Headers.TryGetValue("Sec-WebSocket-Draft", out version))
                return version;

            if (request.Headers.ContainsKey("Sec-WebSocket-Key1"))
                return "76";

            if ((request.Body != null) && request.Body.ToLower().Contains("policy-file-request"))
                return "policy-file-request";

            return "75";
        }
    }
}
