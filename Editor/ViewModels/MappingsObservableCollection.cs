using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraktorMapping.TSI.Format;

namespace Editor.ViewModels
{
    class MappingsObservableCollection : ObservableCollection<MappingModel>
    {
        public MappingsObservableCollection(Device device)
            : base(CreateModel(device))
        {
            
        }

        // FIXME: drilling down from the bindings means that
        //        if there are any unbound traktor mappings (not associated with a midi note)
        //        we won't dispay them in the UI
        private static List<MappingModel> CreateModel(Device device)
        {
            var inMappings = device.Data.Mappings.MidiBindings.Bindings
                .Where(binding => device.Data.MidiDefinitions.In.Definitions.Any(d => d.MidiNote == binding.MidiNote))
                .Select(binding => new {
                    Midi = device.Data.MidiDefinitions.In.Definitions.Single(d => d.MidiNote == binding.MidiNote),
                    Mapping = device.Data.Mappings.List.Mappings.Single(m => m.MidiNoteBindingId == binding.BindingId),
                    Type = MappingModel.MappingType.In,
                });

            var outMappings = device.Data.Mappings.MidiBindings.Bindings
                .Where(binding => device.Data.MidiDefinitions.Out.Definitions.Any(d => d.MidiNote == binding.MidiNote))
                .Select(binding => new {
                    Midi = device.Data.MidiDefinitions.Out.Definitions.Single(d => d.MidiNote == binding.MidiNote),
                    Mapping = device.Data.Mappings.List.Mappings.Single(m => m.MidiNoteBindingId == binding.BindingId),
                    Type = MappingModel.MappingType.Out,
                });

            return inMappings.Concat(outMappings).Select(m => new MappingModel() {
                Id = m.Mapping.MidiNoteBindingId,
                MidiNote = m.Midi.MidiNote,
                TraktorCommand = m.Mapping.TraktorControl.Name,
                Deck = m.Mapping.Settings.Deck.ToString(),
                Type = m.Type,
                Comment = m.Mapping.Settings.Comment,
                TargetType = m.Mapping.TraktorControl.Target,
            }).ToList();
        }
    }
}
