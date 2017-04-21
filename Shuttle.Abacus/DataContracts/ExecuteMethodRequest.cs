using System.Collections.Generic;
using System.Xml.Serialization;

namespace Abacus.DataContracts
{
    public class ExecuteMethodRequest
    {
        public ExecuteMethodRequest()
        {
            ArgumentAnswers = new List<NameValue>();
        }

        [XmlArrayItem("ArgumentAnswer")]
        public List<NameValue> ArgumentAnswers { get; set; }
    }
}
