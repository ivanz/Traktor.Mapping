using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TraktorMapping.TSI.Utils
{
    public static class StreamExtensions
    {
  
        #region Read

        public static byte[] ReadBytes(this Stream stream, int length)
        {
            byte[]bytes = new byte[length];
            stream.Read(bytes, 0, length);

            return bytes;
        }

        public static string ReadASCIIString(this Stream stream, int length)
        {
            return Encoding.ASCII.GetString(stream.ReadBytes(length));
        }

        public static float ReadFloatBigE(this Stream stream)
        {
            byte[]bytes = stream.ReadBytes(4);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);

            return BitConverter.ToSingle(bytes, 0);
        }

        public static int ReadInt32BigE(this Stream stream)
        {
            byte[]bytes = stream.ReadBytes(4);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);

            return BitConverter.ToInt32(bytes, 0);
        }

        public static string ReadWideStringBigE(this Stream stream)
        {
            int length = stream.ReadInt32BigE() * 2;

            return Encoding.BigEndianUnicode.GetString(stream.ReadBytes(length));
        }

        public static bool ReadBoolBigE(this Stream stream)
        {
            return stream.ReadInt32BigE() == 1;
        }

        #endregion

    }
}
