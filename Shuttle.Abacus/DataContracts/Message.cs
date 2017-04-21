using System.Xml.Serialization;

namespace Shuttle.Abacus.DataContracts
{
    public class Message
    {
        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlAttribute("text")]
        public string Text { get; set; }
    }
}
