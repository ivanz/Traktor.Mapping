using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TraktorMapping.TSI.Utils;

namespace TraktorMapping.TSI.Format
{
    public class MappingsContainer : Frame
    {
        public MappingsContainer(Stream stream)
            : base(stream)
        {
            List = new MappingsList(stream);
            MidiBindings = new MidiNoteBindingList(stream);
        }

        public MappingsList List { get; set; }
        public MidiNoteBindingList MidiBindings { get; set; }

        public override void Write(Writer writer)
        {
            writer.BeginFrame(FrameId);

            List.Write(writer);
            MidiBindings.Write(writer);

            writer.EndFrame();
        }
    }
}
