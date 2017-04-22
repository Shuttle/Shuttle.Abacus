using System.Collections.Generic;
using System.Xml.Serialization;
using Shuttle.Abacus.DTO;

namespace Shuttle.Abacus.DataContracts
{
    public class ExecuteMethodResponse
    {
        public ExecuteMethodResponse()
        {
            Messages = new List<Message>();
            CalculatedValues = new List<NameValue>();
            Total = "0";
        }

        public List<Message> Messages { get; set; }
        public List<GraphNodeDTO> GraphNodes { get; set; }

        [XmlArrayItem("CalculatedValue")]
        public List<NameValue> CalculatedValues { get; set; }

        public string Total { get; set; }
    }
}
