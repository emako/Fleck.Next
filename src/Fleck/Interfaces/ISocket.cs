using System;
using System.IO;
using System.Net;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Fleck
{
    public interface ISocket
    {
        public bool Connected { get; }

        public string RemoteIpAddress { get; }

        public int RemotePort { get; }

        public Stream Stream { get; }

        public bool NoDelay { get; set; }

        public EndPoint LocalEndPoint { get; }

        public Task<ISocket> Accept(Action<ISocket> callback, Action<Exception> error);

        public Task Send(byte[] buffer, Action callback, Action<Exception> error);

        public Task<int> Receive(byte[] buffer, Action<int> callback, Action<Exception> error, int offset = 0);

        public Task Authenticate(X509Certificate2 certificate, SslProtocols enabledSslProtocols, Action callback, Action<Exception> error);

        public void Dispose();

        public void Close();

        public void Bind(EndPoint ipLocal);

        public void Listen(int backlog);

        public void SetKeepAlive(bool keepAlive, uint keepAliveTime, uint keepAliveInterval, uint retryCount = 5);
    }
}
