using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TraktorMapping.TSI.Utils;

namespace TraktorMapping.TSI.Format
{
    public class MidiNoteBinding : Frame
    {
        public MidiNoteBinding(Stream stream)
            : base(stream)
        {
            BindingId = stream.ReadInt32BigE();
            MidiNote = stream.ReadWideStringBigE();
        }

        public int BindingId { get; set; }
        public string MidiNote { get; set; }

        public override void Write(Writer writer)
        {
            writer.BeginFrame(FrameId);

            writer.WriteBigE(BindingId);
            writer.WriteWideStringBigE(MidiNote);

            writer.EndFrame();
        }
    }
}
