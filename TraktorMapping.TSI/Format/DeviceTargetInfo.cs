using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TraktorMapping.TSI.Utils;

namespace TraktorMapping.TSI.Format
{
    public class DeviceTargetInfo : Frame
    {
        public DeviceTargetInfo(Stream stream)
            : base(stream)
        {
            Target = (DeviceTarget) stream.ReadInt32BigE();
        }

        public DeviceTarget Target { get; set; }

        public override void Write(Writer writer)
        {
            writer.BeginFrame(FrameId);

            writer.WriteBigE((int)Target);

            writer.EndFrame();
        }
    }
}
