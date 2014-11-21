using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TraktorMapping.TSI.Utils;

namespace TraktorMapping.TSI.Format
{
    public class DevicesList : Frame
    {
        public DevicesList(Stream stream)
            : base(stream)
        {
            int count = stream.ReadInt32BigE();
            List = new List<Device>();

            for (int i = 0; i < count; i++)
                List.Add(new Device(stream));
        }

        public List<Device> List { get; set; }

        public override void Write(Writer writer)
        {
            writer.BeginFrame(FrameId);

            writer.WriteBigE(List.Count);
            foreach (Device device in List)
                device.Write(writer);

            writer.EndFrame();
        }
    }
}
