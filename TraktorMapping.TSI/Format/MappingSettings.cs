using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TraktorMapping.TSI.Utils;

namespace TraktorMapping.TSI.Format
{
    public class MappingSettings : Frame
    {
        public MappingSettings(Stream stream)
            : base(stream)
        {
            Unknown1 = stream.ReadInt32BigE();
            ControllerType = (MappingControllerType) stream.ReadInt32BigE();
            InteractionMode = (MappingInteractionMode) stream.ReadInt32BigE();
            Deck = (MappingTargetDeck) stream.ReadInt32BigE();
            AutoRepeat = stream.ReadBoolBigE();
            Invert = stream.ReadBoolBigE();
            SoftTakeover = stream.ReadBoolBigE();
            RotarySensitivity = stream.ReadFloatBigE();
            RotaryAcceleration = stream.ReadFloatBigE();
            Unknown10 = stream.ReadInt32BigE();
            Unknown11 = stream.ReadInt32BigE();
            SetValueTo = stream.ReadFloatBigE();
            Comment = stream.ReadWideStringBigE();
            ModifierOneId = stream.ReadInt32BigE();
            Unknown15 = stream.ReadInt32BigE();
            ModifierOneValue = stream.ReadInt32BigE();
            ModifierTwoId = stream.ReadInt32BigE();
            Unknown18 = stream.ReadInt32BigE();
            ModifierTwoValue = stream.ReadInt32BigE();
            Unknown20 = stream.ReadInt32BigE();
            LedMinControllerRange = stream.ReadFloatBigE();
            Unknown22 = stream.ReadInt32BigE();
            LedMaxControllerRange = stream.ReadFloatBigE();
            LedMinMidiRange = stream.ReadInt32BigE();
            LedMaxMidiRange = stream.ReadInt32BigE();
            LedInvert = stream.ReadInt32BigE();
            LedBlend = stream.ReadInt32BigE();
            Unknown29 = stream.ReadInt32BigE();
            Resolution = (MappingResolution) stream.ReadInt32BigE();
            Unknown30 = stream.ReadInt32BigE();
        }

        public int Unknown1 { get; set; }
        public MappingControllerType ControllerType { get; set; }
        public MappingInteractionMode InteractionMode { get; set; }
        public MappingTargetDeck Deck { get; set; }
        public bool AutoRepeat { get; set; }
        public bool Invert { get; set; }
        public bool SoftTakeover { get; set; }

        // 1% in the Traktor UI corresponds to 0.5f
        // Traktor sets this to 300% / 15f when 
        // in Interaction mode is Direct
        public float RotarySensitivity { get; set; }
        public float RotaryAcceleration { get; set; }
        public int Unknown10 { get; set; }
        public int Unknown11 { get; set; }
        public float SetValueTo { get; set; }
        public string Comment { get; set; }

        // Traktor Control Id
        public int ModifierOneId { get; set; }
        public int Unknown15 { get; set; }
        public int ModifierOneValue { get; set; }
        public int ModifierTwoId { get; set; }
        public int Unknown18 { get; set; }
        public int ModifierTwoValue { get; set; }
        public int Unknown20 { get; set; }
        public float LedMinControllerRange { get; set; }
        public int Unknown22 { get; set; }
        public float LedMaxControllerRange { get; set; }
        public int LedMinMidiRange { get; set; }
        public int LedMaxMidiRange { get; set; }
        public int LedInvert { get; set; }
        public int LedBlend { get; set; }
        public int Unknown29 { get; set; }
        // this field is actually a public float under the hood
        private MappingResolution Resolution { get; set; }
        public int Unknown30 { get; set; }

        public override void Write(Writer writer)
        {
            writer.BeginFrame(FrameId);

            writer.WriteBigE(Unknown1);
            writer.WriteBigE((int)ControllerType);
            writer.WriteBigE((int)InteractionMode);
            writer.WriteBigE((int)Deck);
            writer.WriteBigE(AutoRepeat);
            writer.WriteBigE(Invert);
            writer.WriteBigE(SoftTakeover);

            // 1% in the Traktor UI corresponds to 0.5f
            // Traktor sets this to 300% / 15f when 
            // in Interaction mode is Direct
            writer.WriteBigE(RotarySensitivity);
            writer.WriteBigE(RotaryAcceleration);
            writer.WriteBigE(Unknown10);
            writer.WriteBigE(Unknown11);
            writer.WriteBigE(SetValueTo);
            writer.WriteWideStringBigE(Comment);

            // Traktor Control Id
            writer.WriteBigE(ModifierOneId);
            writer.WriteBigE(Unknown15);
            writer.WriteBigE(ModifierOneValue);
            writer.WriteBigE(ModifierTwoId);
            writer.WriteBigE(Unknown18);
            writer.WriteBigE(ModifierTwoValue);
            writer.WriteBigE(Unknown20);
            writer.WriteBigE(LedMinControllerRange);
            writer.WriteBigE(Unknown22);
            writer.WriteBigE(LedMaxControllerRange);
            writer.WriteBigE(LedMinMidiRange);
            writer.WriteBigE(LedMaxMidiRange);
            writer.WriteBigE(LedInvert);
            writer.WriteBigE(LedBlend);
            writer.WriteBigE(Unknown29);
            // this field is actually a writer.WriteBE(hood
            writer.WriteBigE((int)Resolution);
            writer.WriteBigE(Unknown30);

            writer.EndFrame();
        }
    }
}
