using System.Collections.Generic;

namespace Fleck
{
    public interface IHandler
    {
        public byte[] CreateHandshake(string subProtocol = null);

        public void Receive(IEnumerable<byte> data);

        public byte[] FrameText(string text);

        public byte[] FrameBinary(byte[] bytes);

        public byte[] FramePing(byte[] bytes);

        public byte[] FramePong(byte[] bytes);

        public byte[] FrameClose(int code);
    }
}
