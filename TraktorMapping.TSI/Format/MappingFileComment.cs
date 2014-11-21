using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TraktorMapping.TSI.Utils;

namespace TraktorMapping.TSI.Format
{
    public class MappingFileComment  : Frame
    {
        public MappingFileComment(Stream stream)
            : base(stream)
        {
            Comment = stream.ReadWideStringBigE();
        }

        public string Comment { get; set; }

        public override void Write(Writer writer)
        {
            writer.BeginFrame(FrameId);

            writer.WriteWideStringBigE(Comment);

            writer.EndFrame();
        }
    }
}
