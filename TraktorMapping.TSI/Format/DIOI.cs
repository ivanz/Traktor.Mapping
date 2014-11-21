using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TraktorMapping.TSI.Utils;

namespace TraktorMapping.TSI.Format
{
    public class DIOI : Frame
    {
        public DIOI(Stream stream)
            : base(stream)
        {
            Unknown = stream.ReadInt32BigE();
        }

        public int Unknown { get; set; }

        public override void Write(Writer writer)
        {
            writer.BeginFrame(FrameId);

            writer.WriteBigE(Unknown);

            writer.EndFrame();
        }
    }
}
