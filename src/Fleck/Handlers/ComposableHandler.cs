using System;
using System.Collections.Generic;

namespace Fleck.Handlers
{
    public class ComposableHandler : IHandler
    {
        public Func<string, byte[]> Handshake = s => [];
        public Func<string, byte[]> TextFrame = x => [];
        public Func<byte[], byte[]> BinaryFrame = x => [];
        public Action<List<byte>> ReceiveData = static delegate { };
        public Func<byte[], byte[]> PingFrame = i => [];
        public Func<byte[], byte[]> PongFrame = i => [];
        public Func<int, byte[]> CloseFrame = i => [];

        private readonly List<byte> _data = [];

        public byte[] CreateHandshake(string subProtocol = null)
        {
            return Handshake(subProtocol);
        }

        public void Receive(IEnumerable<byte> data)
        {
            _data.AddRange(data);

            ReceiveData(_data);
        }

        public byte[] FrameText(string text)
        {
            return TextFrame(text);
        }

        public byte[] FrameBinary(byte[] bytes)
        {
            return BinaryFrame(bytes);
        }

        public byte[] FramePing(byte[] bytes)
        {
            return PingFrame(bytes);
        }

        public byte[] FramePong(byte[] bytes)
        {
            return PongFrame(bytes);
        }

        public byte[] FrameClose(int code)
        {
            return CloseFrame(code);
        }
    }
}
