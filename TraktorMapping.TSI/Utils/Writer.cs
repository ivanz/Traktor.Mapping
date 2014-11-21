using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TraktorMapping.TSI.Utils
{
    public class Writer
    {
        private class FrameTracker
        {
            public readonly int HeaderSize = 2*4; // 4 bytes header + 4 bytes size
            public long SizeOffsetInStream { get; set; }
            public int Size { get; set; }
        }

        private readonly Stack<FrameTracker> _frames = new Stack<FrameTracker>();
        private readonly Stream _stream;
        
        public Writer(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException("stream", "stream is null.");
            _stream = stream;
        }

        public void BeginFrame(string id)
        {
            FrameTracker tracker = new FrameTracker();
            _frames.Push(tracker);
            WriteASCII(id, incrementSize: false);
            tracker.SizeOffsetInStream = _stream.Position;
            WriteBigE(0, incrementSize: false); // Size placeholder
        }

        public void EndFrame()
        {
            FrameTracker tracker = _frames.Pop();

            long currentPosition = _stream.Position;
            _stream.Seek(tracker.SizeOffsetInStream, SeekOrigin.Begin);
            WriteBigE(tracker.Size, incrementSize: false);
            _stream.Seek(currentPosition, SeekOrigin.Begin);

            if (_frames.Count > 0)
                _frames.Peek().Size += tracker.Size + tracker.HeaderSize; // parent frame size increase
        }

        #region Write Utils

        private void WriteBytes(byte[] bytes, bool incrementSize)
        {
            _stream.Write(bytes, 0, bytes.Length);
            if (incrementSize)
                _frames.Peek().Size += bytes.Length;
        }

        public void WriteBytes(byte[] bytes)
        {
            WriteBytes(bytes, incrementSize: true);
        }

        private void WriteBigE(int value, bool incrementSize)
        {
            byte[]bytes = BitConverter.GetBytes(value);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);

            WriteBytes(bytes, incrementSize);
        }

        public void WriteBigE(int value)
        {
            WriteBigE(value, incrementSize: true);
        }

        public void WriteBigE(bool value)
        {
            WriteBigE(value ? 1 : 0);
        }

        public void WriteBigE(float value)
        {
            byte[]bytes = BitConverter.GetBytes(value);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);

            WriteBytes(bytes);
        }

        private void WriteASCII(string value, bool incrementSize)
        {
            WriteBytes(Encoding.ASCII.GetBytes(value), incrementSize);
        }

        public void WriteASCII(string value)
        {
            WriteBytes(Encoding.ASCII.GetBytes(value), incrementSize: true);
        }

        public void WriteWideStringBigE(string value)
        {
            int length;
            if (value == null)
                length = 0;
            else
                length = value.Length;

            WriteBigE(length);
            WriteBytes(Encoding.BigEndianUnicode.GetBytes(value));
        }
        #endregion
    }
}
