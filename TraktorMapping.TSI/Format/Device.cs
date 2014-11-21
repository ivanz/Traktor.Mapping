using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TraktorMapping.TSI.Utils;

namespace TraktorMapping.TSI.Format
{
    public class Device : Frame
    {
        public Device(Stream stream)
            : base(stream)
        {
            Name = stream.ReadWideStringBigE();
            Data = new DeviceData(stream);
        }

        public string Name { get; set; }
        public DeviceData Data { get; set; }

        public override void Write(Writer writer)
        {
            writer.BeginFrame(FrameId);

            writer.WriteWideStringBigE(Name);

            Data.Write(writer);
            writer.EndFrame();
        }
    }
}
