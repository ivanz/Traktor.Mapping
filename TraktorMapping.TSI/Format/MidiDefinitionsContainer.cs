using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TraktorMapping.TSI.Utils;

namespace TraktorMapping.TSI.Format
{
    public class MidiDefinitionsContainer : Frame
    {
        public MidiDefinitionsContainer(Stream stream)
            : base(stream)
        {
            In = new MidiInDefinitions(stream);
            Out = new MidiOutDefinitions(stream);
        }

        public MidiInDefinitions In { get; set; }
        public MidiOutDefinitions Out { get; set; }

        public override void Write(Writer writer)
        {
            writer.BeginFrame(FrameId);

            In.Write(writer);
            Out.Write(writer);

            writer.EndFrame();
        }
    }
}
