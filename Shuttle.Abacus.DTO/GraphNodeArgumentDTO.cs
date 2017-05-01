using System.Xml.Serialization;

namespace Shuttle.Abacus.DTO
{
    public class GraphNodeDataRow
    {
        [XmlAttribute("argument")]
        public string Argument { get; set; }

        [XmlAttribute("display")]
        public string Display { get; set; }
    }
}
