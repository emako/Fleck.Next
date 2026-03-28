using System;

namespace Fleck
{
    public interface IWebSocketServer : IDisposable
    {
        public void Start(Action<IWebSocketConnection> config);
    }
}
