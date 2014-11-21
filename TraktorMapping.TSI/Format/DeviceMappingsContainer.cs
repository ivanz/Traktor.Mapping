using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TraktorMapping.TSI.Utils;

namespace TraktorMapping.TSI.Format
{
    public class DeviceMappingsContainer : Frame
    {
        public DeviceMappingsContainer(Stream stream)
            : base(stream)
        {
            DIOI = new DIOI(stream);
            Devices = new DevicesList(stream);
        }

        public DIOI DIOI { get; set; }
        public DevicesList Devices { get; set; }

        public override void Write(Writer writer)
        {
            writer.BeginFrame(FrameId);

            DIOI.Write(writer);
            Devices.Write(writer);

            writer.EndFrame();
        }
    }
}
