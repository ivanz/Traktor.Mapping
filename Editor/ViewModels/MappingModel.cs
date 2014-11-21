using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraktorMapping.TSI.Format;

namespace Editor.ViewModels
{
    class MappingModel
    {
        public int Id { get; set; }
        public string MidiNote { get; set; }
        public string TraktorCommand { get; set; }
        public TargetType TargetType { get; set; }
        public string Comment { get; set; }
        public string Condition1 { get; set; }
        public string Condition2 { get; set; }
        public string Deck { get; set; }
        public MappingModel.MappingType Type { get; set; }

        internal enum MappingType
        {
            In,
            Out
        }
    }
}
