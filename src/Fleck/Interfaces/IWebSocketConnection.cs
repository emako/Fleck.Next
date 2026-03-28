using System;
using System.Threading.Tasks;

namespace Fleck
{
    public interface IWebSocketConnection
    {
        public Action OnOpen { get; set; }

        public Action OnClose { get; set; }

        public Action<string> OnMessage { get; set; }

        public Action<byte[]> OnBinary { get; set; }

        public Action<byte[]> OnPing { get; set; }

        public Action<byte[]> OnPong { get; set; }

        public Action<Exception> OnError { get; set; }

        public Task Send(string message);

        public Task Send(byte[] message);

        public Task SendPing(byte[] message);

        public Task SendPong(byte[] message);

        public void Close();

        public void Close(int code);

        public void SetKeepAlive(bool keepAlive, uint keepAliveTime, uint keepAliveInterval, uint retryCount = 5);

        IWebSocketConnectionInfo ConnectionInfo { get; }

        public bool IsAvailable { get; }
    }
}
