using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TraktorMapping.TSI.Utils;

namespace TraktorMapping.TSI.Format
{
    public abstract class Frame
    {
        private const int FRAME_ID_FIXED_LENGTH = 4;

        protected Frame(Stream stream)
        {
            FrameId = stream.ReadASCIIString(4);
            FrameSizeOnDisk = stream.ReadInt32BigE();
        }

        protected Frame(string id)
        {
            if (String.IsNullOrEmpty(id) || id.Length != FRAME_ID_FIXED_LENGTH)
                throw new ArgumentException("id must be 4 characters exactly");
        }

        public string FrameId { get; protected set; }

        protected int? FrameSizeOnDisk { get; private set; }

        public abstract void Write(Writer writer);
    }
}