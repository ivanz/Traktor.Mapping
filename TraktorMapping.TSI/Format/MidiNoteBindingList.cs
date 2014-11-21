using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TraktorMapping.TSI.Utils;

namespace TraktorMapping.TSI.Format
{
    public class MidiNoteBindingList : Frame
    {
        public MidiNoteBindingList(Stream stream)
            : base(stream)
        {
            int count = stream.ReadInt32BigE();
            Bindings = new List<MidiNoteBinding>();

            for (int i = 0; i < count; i++)
                Bindings.Add(new MidiNoteBinding(stream));
        }

        public List<MidiNoteBinding> Bindings { get; set; }

        public override void Write(Writer writer)
        {
            writer.BeginFrame(FrameId);

            writer.WriteBigE(Bindings.Count);
            foreach (MidiNoteBinding item in Bindings)
                item.Write(writer);

            writer.EndFrame();
        }
    };
}
