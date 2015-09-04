using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace TraktorMapping.TSI.Format
{
    public class TraktorControl
    {
        public static readonly TraktorControl Unknown = new TraktorControl(-1, "Unknown", "Unknown", TargetType.Unknown);

        private static IReadOnlyCollection<TraktorControl> _allIn;
        private static IReadOnlyCollection<TraktorControl> _allOut;
        private static IReadOnlyCollection<TraktorControl> _all;

        public static IReadOnlyCollection<TraktorControl> AllIn {
            get {
                if (_allIn == null) {
                    _allIn = XDocument
                        .Load(new StringReader(Resources.commands))
                        .Element("commands")
                        .Element("in")
                        .Elements()
                        .Select(c => new TraktorControl(Int32.Parse(c.Attribute("id").Value), 
                                                        c.Attribute("name").Value, 
                                                        c.Attribute("category").Value, 
                                                        (TargetType)Enum.Parse(typeof(TargetType), c.Attribute("target").Value)))
                        .ToList()
                        .AsReadOnly();
                }

                return _allIn;
            }
        }

        public static IReadOnlyCollection<TraktorControl> AllOut {
            get {
                if (_allOut == null) {
                    _allOut = XDocument
                        .Load(new StringReader(Resources.commands))
                        .Element("commands")
                        .Element("out")
                        .Elements()
                        .Select(c => new TraktorControl(Int32.Parse(c.Attribute("id").Value),
                                                        c.Attribute("name").Value,
                                                        c.Attribute("category").Value,
                                                        (TargetType) Enum.Parse(typeof(TargetType), c.Attribute("target").Value)))
                        .ToList()
                        .AsReadOnly();
                }

                return _allOut;
            }
        }

        public static IReadOnlyCollection<TraktorControl> All {
            get { 
                if (_all == null)
                    _all = AllIn.Concat(AllOut).ToList().AsReadOnly();

                return _all;
            }
        }

        private TraktorControl(int id, string name, string category, TargetType target)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentException("name is null or empty.", nameof(name));
            if (String.IsNullOrEmpty(category))
                throw new ArgumentException("category is null or empty.", nameof(category));

            Id = id;
            Name = name;
            Category = category;
            Target = target;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Category { get; private set; }
        public TargetType Target { get; set; }
    }
}