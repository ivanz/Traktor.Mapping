using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TraktorMapping.TSI.Utils;

namespace TraktorMapping.TSI.Format
{
    public class DeviceData : Frame
    {
        public DeviceData(Stream stream)
            : base(stream)
        {
            Target = new DeviceTargetInfo(stream);
            Version = new VersionInfo(stream);
            Comment = new MappingFileComment(stream);
            Ports = new DevicePorts(stream);
            MidiDefinitions = new MidiDefinitionsContainer(stream);
            Mappings = new MappingsContainer(stream);
            Unknown = new DVST(stream);
        }

        public DeviceTargetInfo Target { get; set; }
        public VersionInfo Version { get; set; }
        public MappingFileComment Comment { get; set; }
        public DevicePorts Ports { get; set; }
        public MidiDefinitionsContainer MidiDefinitions { get; set; }
        public MappingsContainer Mappings { get; set; }
        public DVST Unknown { get; set; }

        public override void Write(Writer writer)
        {
            writer.BeginFrame(FrameId);

            Target.Write(writer);
            Version.Write(writer);
            Comment.Write(writer);
            Ports.Write(writer);
            MidiDefinitions.Write(writer);
            Mappings.Write(writer);
            Unknown.Write(writer);

            writer.EndFrame();
        }
    }
}
