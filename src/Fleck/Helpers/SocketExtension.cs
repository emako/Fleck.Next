using System;
using System.Net.Sockets;

namespace Fleck.Helpers
{
    internal static class SocketExtension
    {
        /// <summary>
        /// Enables or disables TCP keep-alive on a socket and configures related options.
        /// </summary>
        /// <param name="socket">The underlying TCP <see cref="Socket"/> to configure.</param>
        /// <param name="keepAlive">Whether to enable (<c>true</c>) or disable (<c>false</c>) TCP keep-alive.</param>
        /// <param name="keepAliveTime">The idle time in milliseconds before the first keep-alive probe is sent.</param>
        /// <param name="keepAliveInterval">The interval in milliseconds between subsequent keep-alive probes when no acknowledgment is received.</param>
        /// <param name="retryCount">The number of keep-alive probes to send before the connection is considered dead (where supported).</param>
        public static void SetKeepAlive(this Socket socket, bool keepAlive, uint keepAliveTime, uint keepAliveInterval, uint retryCount = 5)
        {
            if (FleckRuntime.IsRunningOnWindows())
            {
#if NETCOREAPP3_0_OR_GREATER
                socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, keepAlive);
                socket.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.TcpKeepAliveTime, keepAliveTime / 1000); // Seconds
                socket.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.TcpKeepAliveInterval, keepAliveInterval / 1000); // Seconds
                socket.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.TcpKeepAliveRetryCount, retryCount);
#else
                socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
                SetKeepAliveWindows(socket, keepAlive, keepAliveTime, keepAliveInterval);

                static void SetKeepAliveWindows(Socket socket, bool keepAlive, uint keepAliveInterval, uint retryInterval)
                {
                    int size = sizeof(uint);
                    uint on = keepAlive ? 1u : 0u;

                    byte[] inArray = new byte[size * 3];
                    Array.Copy(BitConverter.GetBytes(on), 0, inArray, 0, size);
                    Array.Copy(BitConverter.GetBytes(keepAliveInterval), 0, inArray, size, size);
                    Array.Copy(BitConverter.GetBytes(retryInterval), 0, inArray, size * 2, size);
                    socket.IOControl(IOControlCode.KeepAliveValues, inArray, null);
                }

                // TcpKeepAliveRetryCount:
                // We are not planning to adjust the .NET Framework-based in registry, so we need to set the retry count here.
                // HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\Tcpip\Parameters / TcpMaxDataRetransmissions ≈ 5
#endif
            }
            else
            {
#if NETCOREAPP3_0_OR_GREATER
                socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
                socket.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.TcpKeepAliveTime, 60);
                socket.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.TcpKeepAliveInterval, 10);
                socket.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.TcpKeepAliveRetryCount, retryCount);
#endif
            }
        }
    }
}
