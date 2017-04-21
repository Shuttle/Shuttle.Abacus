using System.Xml.Serialization;

namespace Shuttle.Abacus.DTO
{
    public class GraphNodeArgumentDTO
    {
        [XmlAttribute("argument")]
        public string Argument { get; set; }

        [XmlAttribute("display")]
        public string Display { get; set; }
    }
}
