using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TraktorMapping.TSI.Utils;

namespace TraktorMapping.TSI.Format
{
    public class VersionInfo : Frame
    {
        public VersionInfo(Stream stream)
            : base(stream)
        {
            Version = stream.ReadWideStringBigE();
            MappingFileRevision = stream.ReadInt32BigE();
        }

        public string Version { get; set; }

        public int MappingFileRevision { get; set; }

        public override void Write(Writer writer)
        {
            writer.BeginFrame(FrameId);

            writer.WriteWideStringBigE(Version);
            writer.WriteBigE(MappingFileRevision);

            writer.EndFrame();
        }
    }
}
