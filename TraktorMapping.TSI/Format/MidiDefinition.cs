using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TraktorMapping.TSI.Utils;

namespace TraktorMapping.TSI.Format
{
    public class MidiDefinition : Frame
    {
        public MidiDefinition(Stream stream)
            : base(stream)
        {
            MidiNote = stream.ReadWideStringBigE();
            Unknown1 = stream.ReadInt32BigE();
            Unknown2 = stream.ReadInt32BigE();
            Velocity = stream.ReadFloatBigE();
            EncoderMode = (MidiEncoderMode) stream.ReadInt32BigE();
            ControlId = stream.ReadInt32BigE();
        }


        public string MidiNote { get; set; }
        public int Unknown1 { get; set; }
        public int Unknown2 { get; set; }
        public float Velocity { get; set; }
        public MidiEncoderMode EncoderMode { get; set; }

        /// <summary>
        /// In the case of Native Instruments devices seems to identify the 
        /// control Id. However this control Id will be the same for e.g. both 
        /// left and right Shift keys (Kontrol S4). Otherwise 0xFFFFFFFF.
        /// </summary>
        public int ControlId { get; set; }


        public override void Write(Writer writer)
        {
            writer.BeginFrame(FrameId);

            writer.WriteWideStringBigE(MidiNote);
            writer.WriteBigE(Unknown1);
            writer.WriteBigE(Unknown2);
            writer.WriteBigE(Velocity);
            writer.WriteBigE((int)EncoderMode);
            writer.WriteBigE(ControlId);

            writer.EndFrame();
        }
    };
}
