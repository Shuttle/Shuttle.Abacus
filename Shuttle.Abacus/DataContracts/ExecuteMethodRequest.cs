using System.Collections.Generic;
using System.Xml.Serialization;

namespace Shuttle.Abacus.DataContracts
{
    public class ExecuteMethodRequest
    {
        public ExecuteMethodRequest()
        {
            ArgumentAnswers = new List<NameValue>();
        }

        [XmlArrayItem("ValueType")]
        public List<NameValue> ArgumentAnswers { get; set; }
    }
}
