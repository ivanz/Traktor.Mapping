using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TraktorMapping.TSI.Utils;

namespace TraktorMapping.TSI.Format
{
    public class DevicePorts : Frame
    {
        public DevicePorts(Stream stream)
            : base(stream)
        {
            InPortName = stream.ReadWideStringBigE();
            OutPortName = stream.ReadWideStringBigE();
        }

        public string InPortName { get; set; }
        public string OutPortName { get; set; }

        public override void Write(Writer writer)
        {
            writer.BeginFrame(FrameId);

            writer.WriteWideStringBigE(InPortName);
            writer.WriteWideStringBigE(OutPortName);

            writer.EndFrame();
        }
    }
}
