using System.Xml.Serialization;

namespace Abacus.DataContracts
{
    public class Message
    {
        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlAttribute("text")]
        public string Text { get; set; }
    }
}
