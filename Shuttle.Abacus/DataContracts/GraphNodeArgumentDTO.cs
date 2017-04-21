using System.Xml.Serialization;

namespace Abacus.DataContracts
{
    public class GraphNodeArgumentDTO
    {
        [XmlAttribute("argument")]
        public string Argument { get; set; }

        [XmlAttribute("display")]
        public string Display { get; set; }
    }
}
