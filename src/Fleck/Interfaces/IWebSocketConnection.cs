using System;
using System.Threading.Tasks;

namespace Fleck
{
    public interface IWebSocketConnection
    {
        Action OnOpen { get; set; }
        Action OnClose { get; set; }
        Action<string> OnMessage { get; set; }
        Action<byte[]> OnBinary { get; set; }
        Action<byte[]> OnPing { get; set; }
        Action<byte[]> OnPong { get; set; }
        Action<Exception> OnError { get; set; }
        Task Send(string message);
        Task Send(byte[] message);
        Task SendPing(byte[] message);
        Task SendPong(byte[] message);
        void Close();
        void Close(int code);
        void SetKeepAlive(bool keepAlive, uint keepAliveTime, uint keepAliveInterval, uint retryCount);
        IWebSocketConnectionInfo ConnectionInfo { get; }
        bool IsAvailable { get; }
    }
}
