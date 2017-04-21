using System.Collections.Generic;
using System.Xml.Serialization;

namespace Abacus.DataContracts
{
    public class GraphNodeDTO
    {
        public GraphNodeDTO()
        {
            GraphNodes = new List<GraphNodeDTO>();
            GraphNodeArguments = new List<GraphNodeArgumentDTO>();
        }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("total")]
        public decimal Total { get; set; }

        [XmlAttribute("subTotal")]
        public decimal SubTotal { get; set; }

        [XmlArrayItem("GraphNode")]
        public List<GraphNodeDTO> GraphNodes { get; set; }

        [XmlArrayItem("GraphNodeArgument")]
        public List<GraphNodeArgumentDTO> GraphNodeArguments { get; set; }

        public void AddGraphNodeArgument(string argument, string display)
        {
            GraphNodeArguments.Add(new GraphNodeArgumentDTO
                                   {
                                       Argument = argument, Display = display
                                   });
        }
    }
}
