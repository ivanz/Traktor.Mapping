using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Xml.XPath;
using TraktorMapping.TSI.Format;
using TraktorMapping.TSI.Utils;

namespace TraktorMapping.TSI
{
    public class TsiFile : IDisposable
    {
        private const string XPATH_TO_DATA = "/NIXML/TraktorSettings/Entry[@Name='DeviceIO.Config.Controller']";

        private readonly DeviceMappingsContainer _devicesContainer;
        private readonly Stream _source;

        public TsiFile(string filePath) : this(File.Open(filePath, FileMode.Open, FileAccess.ReadWrite))
        {
        }

        public TsiFile(Stream source)
        {
            string data = XDocument
                          .Load(source)
                          .XPathSelectElement(XPATH_TO_DATA)
                          .Attribute("Value")
                          .Value;

            byte[] decoded = Convert.FromBase64String(data);

            _devicesContainer = new DeviceMappingsContainer(new MemoryStream(decoded));
            Devices = _devicesContainer.Devices.List.AsReadOnly();
            _source = source;
        }

        public IReadOnlyCollection<Device> Devices { get; private set; }

        public void Save(Stream destination)
        {
            MemoryStream stream = new MemoryStream();

            Writer writer = new Writer(stream);
            _devicesContainer.Write(writer);

            stream.Seek(0, SeekOrigin.Begin);

            byte[] data = new byte[stream.Length];
            stream.Read(data, 0, data.Length);

            string tsiData = Convert.ToBase64String(data, Base64FormattingOptions.None);

            string fileContent;

            using (StreamReader reader = new StreamReader(_source)) {
                fileContent = reader.ReadToEnd();
            }

            destination.Seek(0, SeekOrigin.Begin);
            using (var streamWriter = new StreamWriter(destination)) {
                string injected = Regex.Replace(fileContent,
                              "<Entry Name=\"DeviceIO.Config.Controller\"(.*)Value=\".*\"",
                              String.Format("<Entry Name=\"DeviceIO.Config.Controller\"$1Value=\"{0}\"", tsiData));
                streamWriter.Write(injected);
            }
        }

        public void Save()
        {
            Save(_source);
        }

        #region IDisposable Members

        public void Dispose()
        {
            _source.Dispose();
        }

        #endregion

    }
}
