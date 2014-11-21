using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TraktorMapping.TSI.Utils;

namespace TraktorMapping.TSI.Format
{
    public class MappingsList : Frame
    {
        public MappingsList(Stream stream)
            : base(stream)
        {
            int count = stream.ReadInt32BigE();
            Mappings = new List<Mapping>();

            for (int i = 0; i < count; i++)
                Mappings.Add(new Mapping(stream));
        }

        public List<Mapping> Mappings { get; set; }

        public override void Write(Writer writer)
        {
            writer.BeginFrame(FrameId);

            writer.WriteBigE(Mappings.Count);
            foreach (Mapping item in Mappings)
                item.Write(writer);

            writer.EndFrame();
        }
    }
}
