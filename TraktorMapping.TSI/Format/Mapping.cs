using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TraktorMapping.TSI.Utils;

namespace TraktorMapping.TSI.Format
{
    public class Mapping : Frame
    {
        public Mapping(Stream stream)
            : base(stream)
        {
            MidiNoteBindingId = stream.ReadInt32BigE();
            Type = (MappingType)stream.ReadInt32BigE();
            TraktorControlId = stream.ReadInt32BigE();
            Settings = new MappingSettings(stream);
        }

        public int MidiNoteBindingId { get; set; }

        // Seems to always be 0x00000000
        public MappingType Type { get; set; }

        // Basically what UI Control this mapping is for
        public int TraktorControlId { get; set; }

        public TraktorControl TraktorControl {
            get {
                return TraktorControl.All.FirstOrDefault(c => c.Id == TraktorControlId) ?? TraktorControl.Unknown;
            }
        }

        public MappingSettings Settings { get; set; }


        public override void Write(Writer writer)
        {
            writer.BeginFrame(FrameId);

            writer.WriteBigE(MidiNoteBindingId);
            writer.WriteBigE((int)Type);
            writer.WriteBigE(TraktorControlId);
            Settings.Write(writer);

            writer.EndFrame();
        }
    }
}
