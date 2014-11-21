using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TraktorMapping.TSI.Utils;

namespace TraktorMapping.TSI.Format
{
    public class MidiInDefinitions : Frame
    {
        public MidiInDefinitions(Stream stream)
            : base(stream)
        {
            int count = stream.ReadInt32BigE();
            Definitions = new List<MidiDefinition>();

            for (int i = 0; i < count; i++)
                Definitions.Add(new MidiDefinition(stream));
        }

        public List<MidiDefinition> Definitions { get; set; }

        public override void Write(Writer writer)
        {
            writer.BeginFrame(FrameId);

            writer.WriteBigE(Definitions.Count);
            foreach (MidiDefinition item in Definitions)
                item.Write(writer);

            writer.EndFrame();
        }
    };
}
